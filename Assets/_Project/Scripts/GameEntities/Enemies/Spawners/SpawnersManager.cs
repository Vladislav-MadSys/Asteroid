using System;
using System.Collections.Generic;
using _Project.Scripts.Addressables;
using _Project.Scripts.Config;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class SpawnersManager : ITickable, IInitializable
    {
        [SerializeField] private SpawnerSettings[] Settings;
        
        private Camera _mainCamera;
        private float _timer;
        private EnemyDeathListener _enemyDeathListener;
        private PlayerFactory _playerFactory;
        private GameSessionData _gameSessionData;
        private ConfigData _configData;
        private IResourcesService _resourcesService;
        
        private List<EnemySpawner> _obstaclesSpawner = new List<EnemySpawner>();
        
        public SpawnersManager(
            Camera mainCamera, 
            PlayerFactory playerFactory,
            EnemyDeathListener enemyDeathListener, 
            GameSessionData gameSessionData, 
            SpawnerSettings[] spawnerSettings,
            IResourcesService resourcesService,
            ConfigData configData)
        {
            _mainCamera = mainCamera;
            _playerFactory = playerFactory;
            _enemyDeathListener = enemyDeathListener;
            _gameSessionData = gameSessionData;
            _resourcesService = resourcesService;
            _configData = configData;
            
            Settings = spawnerSettings;
        }

        public async void Initialize()
        {
            foreach (SpawnerSettings spawnerSettings in Settings)
            {
                ObjectPool<Enemy> objectPool;
                var asteroidTask = _resourcesService.Load<GameObject>(AddressablesKeys.ASTEROID);
                var ufoTask = _resourcesService.Load<GameObject>(AddressablesKeys.UFO);
                var (asteroidPrefab, ufoPrefab) = await UniTask.WhenAll(asteroidTask, ufoTask);
                    
                
                switch (spawnerSettings.Type)
                {
                    case SpawnerType.Asteroid:
                        objectPool = new ObjectPool<Enemy>(asteroidPrefab);
                        objectPool.Initialize();
                        
                        AsteroidSpawner asteroidSpawner = new AsteroidSpawner();
                        asteroidSpawner.Initialize(_mainCamera,spawnerSettings, objectPool, _enemyDeathListener, _resourcesService, _playerFactory, _gameSessionData, _configData);
                        _obstaclesSpawner.Add(asteroidSpawner);
                        break;
                    case SpawnerType.Ufo:
                        objectPool = new ObjectPool<Enemy>(ufoPrefab);
                        objectPool.Initialize();
                        UfoSpawner ufoSpawner = new UfoSpawner();
                        ufoSpawner.Initialize(_playerFactory, _mainCamera, spawnerSettings, objectPool, _enemyDeathListener, _gameSessionData, _configData);
                        _obstaclesSpawner.Add(ufoSpawner);
                        break;
                }
            }
        }

        public void Tick()
        {
            foreach (var spawner in _obstaclesSpawner)
            {
                spawner.Tick();
            }
        }
    }
}
