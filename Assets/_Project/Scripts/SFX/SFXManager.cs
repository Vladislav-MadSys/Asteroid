using System;
using _Project.Scripts.Addressables;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.SFX
{
    public class SFXManager : IInitializable, IDisposable
    {
        private AudioClip _playerShotSound;
        private AudioClip _enemyExplosionSound;
    
        private PlayerStates _playerStates;
        private EnemyDeathListener _enemyDeathListener;
        private IResourcesService _resourcesService;
    
        
        public SFXManager(PlayerStates playerStates, EnemyDeathListener enemyDeathListener, IResourcesService resourcesService)
        {
            _playerStates = playerStates;
            _enemyDeathListener = enemyDeathListener;
            _resourcesService = resourcesService;
        }

       public async void Initialize()
       {
           _playerShotSound = await _resourcesService.Load<AudioClip>(AddressablesKeys.PLAYER_SHOOT_SFX);
           _enemyExplosionSound = await _resourcesService.Load<AudioClip>(AddressablesKeys.ENEMY_DEATH_SFX);
            
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
            AudioSource.PlayClipAtPoint(_playerShotSound, position, 1);
        }

        private void PlayEnemyExplosionEffect(Vector3 position)
        {
            AudioSource.PlayClipAtPoint(_enemyExplosionSound, position, 1);
        }
    }
}
