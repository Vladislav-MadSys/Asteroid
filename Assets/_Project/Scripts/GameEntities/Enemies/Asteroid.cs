using System;
using System.Threading;
using _Project.Scripts.Addressables;
using _Project.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies
{
    public class Asteroid : Enemy
    {
        [SerializeField] private int _debrisCount = 3;
        [SerializeField] private int _lifeTimeMs = 10000;

        private GameObject _debrisPrefab;
        private CancellationTokenSource _cancellationTokenSource;


        public async override void Initialize(
            EnemyDeathListener enemyDeathListener, 
            GameSessionData gameSessionData, 
            IResourcesService resourcesService, 
            ConfigData configData,
            bool isFromPool)
        {
            base.Initialize(enemyDeathListener, gameSessionData, resourcesService, configData, isFromPool);
            _debrisPrefab = await _resourcesService.Load<GameObject>(AddressablesKeys.PART_OF_ASTEROID);
        }

        private async void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await UniTask.Delay(_lifeTimeMs, cancellationToken: _cancellationTokenSource.Token);
                Kill();
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }
        
        public override void Kill()
        {
            _cancellationTokenSource.Cancel();
            if (_debrisCount > 0)
            {
                for (int i = 0; i < _debrisCount; i++)
                {
                    if (_debrisPrefab != null)
                    {
                        GameObject deathObject = Instantiate(_debrisPrefab, transform);

                        deathObject.transform.parent = transform.parent;

                        if (deathObject.TryGetComponent(out AsteroidMovement asteroidMovement))
                        {
                            asteroidMovement.SetStartDirection();
                        }

                        if (deathObject.TryGetComponent(out Enemy enemy))
                        {
                            enemy.Initialize(_enemyDeathListener, _gameSessionData, _resourcesService, _config, false);
                        }
                    }
                }
            }
            _gameSessionData.AddDestroyedAsteroids();
            base.Kill();
        }
        
    }
}