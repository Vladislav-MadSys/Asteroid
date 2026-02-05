using System;
using System.Threading;
using _Project.Scripts.Addressables;
using _Project.Scripts.Config;
using _Project.Scripts.GameEntities.Player;
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
        
        private bool _isInitialized;


        public async override void Initialize(
            EnemyDeathListener enemyDeathListener, 
            GameSessionData gameSessionData, 
            IResourcesService resourcesService,
            PlayerFactory playerFactory,
            ConfigData configData,
            bool isFromPool)
        {
            base.Initialize(enemyDeathListener, gameSessionData, resourcesService, playerFactory, configData, isFromPool);
            _debrisPrefab = await _resourcesService.Load<GameObject>(AddressablesKeys.PART_OF_ASTEROID);
            _cancellationTokenSource = new CancellationTokenSource();
            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                InitSelfDestroy();
                
                _gameSessionData.OnPlayerKilled += OnPlayerKilled;
                _gameSessionData.OnPlayerRespawned += OnPlayerRespawned;
            }
        }
        private void OnDisable()
        {
            if (_isInitialized)
            {
                _cancellationTokenSource.Cancel();

                _gameSessionData.OnPlayerKilled -= OnPlayerKilled;
                _gameSessionData.OnPlayerRespawned -= OnPlayerRespawned;
            }
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
                            asteroidMovement.Initialize(_configData, _playerFactory);
                            asteroidMovement.SetStartDirection();
                        }

                        if (deathObject.TryGetComponent(out Enemy enemy))
                        {
                            enemy.Initialize(_enemyDeathListener, _gameSessionData, _resourcesService, _playerFactory, _configData, false);
                        }
                    }
                }
            }
            _gameSessionData.AddDestroyedAsteroids();
            base.Kill();
        }
        
        private async void InitSelfDestroy()
        {
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

        private void OnPlayerKilled()
        {
            _cancellationTokenSource.Cancel();
        }

        private void OnPlayerRespawned()
        {
            InitSelfDestroy();
        }
    }
}