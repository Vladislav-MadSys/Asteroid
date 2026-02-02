using System;
using System.ComponentModel.Design;
using System.Threading;
using _Project.Scripts.Addressables;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.VFX
{
    public class VFXManager : IInitializable, IDisposable
    {
        private ParticleSystem _shootVFX;
        private ParticleSystem _explosionVFX;
 
        private IResourcesService _resourceService;
        private PlayerStates _playerStates;
        private EnemyDeathListener _enemyDeathListener;
    
        private ObjectPool<ParticleSystem> _shootVFXPool;
        private ObjectPool<ParticleSystem> _explosionVFXPool;
    
        private CancellationTokenSource _cancellationTokenSource;
    
        private VFXManager(PlayerStates playerStates, EnemyDeathListener enemyDeathListener, IResourcesService resourceService)
        {
            _resourceService = resourceService;
            _playerStates = playerStates;
            _enemyDeathListener = enemyDeathListener;
        }

        public async void Initialize()
        {
            _shootVFX = await _resourceService.Load<ParticleSystem>(AddressablesKeys.PLAYER_SHOOT_VFX);
            _explosionVFX = await _resourceService.Load<ParticleSystem>(AddressablesKeys.ENEMY_DEATH_VFX);
        
            _shootVFXPool = new ObjectPool<ParticleSystem>(_shootVFX.gameObject, 10);
            _explosionVFXPool = new ObjectPool<ParticleSystem>(_explosionVFX.gameObject, 10);
        
            _shootVFXPool.Initialize();
            _explosionVFXPool.Initialize();
        
            _cancellationTokenSource = new CancellationTokenSource();

            _playerStates.OnPlayerShoot += PlayPlayerShootEffect;
            _enemyDeathListener.OnEnemyDeath += PlayEnemyExplosionEffect;
        }

        public void Dispose()
        {
            _playerStates.OnPlayerShoot -= PlayPlayerShootEffect;
            _enemyDeathListener.OnEnemyDeath -= PlayEnemyExplosionEffect;
        }
    
        private void PlayPlayerShootEffect(Vector3 position, Quaternion rotation)
        {
            ParticleSystem shootVFX = _shootVFXPool.GetObject();
            shootVFX.gameObject.SetActive(true);
            Transform shootTransform = shootVFX.transform;
            shootTransform.position = position;
            shootTransform.rotation = rotation;
            ReturnParticleToPoolAfterDelay(shootVFX.main.duration, _shootVFXPool, shootVFX).Forget();
        }
    
        private void PlayEnemyExplosionEffect(Vector3 position)
        {
            ParticleSystem explosionVFX = _explosionVFXPool.GetObject();
            explosionVFX.gameObject.SetActive(true);
            Transform explosionTransform = explosionVFX.transform;
            explosionTransform.position = position;
            ReturnParticleToPoolAfterDelay(explosionVFX.main.duration, _explosionVFXPool, explosionVFX).Forget();
        }

        private async UniTask ReturnParticleToPoolAfterDelay(float delayInSeconds, ObjectPool<ParticleSystem> poolToReturn, ParticleSystem vfxToReturn)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayInSeconds), cancellationToken: _cancellationTokenSource.Token);
            if(vfxToReturn == null) return;
            vfxToReturn.gameObject.SetActive(false);
            poolToReturn.ReturnObject(vfxToReturn);
        }
    }
}
