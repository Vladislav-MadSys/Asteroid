using System.Collections.Generic;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class SpawnersManager : ITickable, IInitializable
    {
        [SerializeField] private SpawnerSettings[] Settings;
        
        private Camera _mainCamera;
        private PlayerShip _playerShip;
        private float _timer;
        private EnemyDeathListener _enemyDeathListener;
        private GameSessionData _gameSessionData;
        
        private List<EnemySpawner> _obstaclesSpawner = new List<EnemySpawner>();
        
        [Inject]
        private void Inject(PlayerShip playerShip, Camera mainCamera, EnemyDeathListener enemyDeathListener, GameSessionData gameSessionData, SpawnerSettings[] spawnerSettings)
        {
            _playerShip = playerShip;
            _mainCamera = mainCamera;
            _enemyDeathListener = enemyDeathListener;
            _gameSessionData = gameSessionData;
            Settings = spawnerSettings;
        }

        public void Initialize()
        {
            foreach (SpawnerSettings spawnerSettings in Settings)
            {
                ObjectPooler objectPooler = new ObjectPooler(spawnerSettings.Prefab);
                objectPooler.Initialize();
                switch (spawnerSettings.Type)
                {
                    case SpawnerType.Asteroid:
                        AsteroidSpawner asteroidSpawner = new AsteroidSpawner();
                        asteroidSpawner.Initialize(_mainCamera,spawnerSettings, objectPooler, _enemyDeathListener, _gameSessionData);
                        _obstaclesSpawner.Add(asteroidSpawner);
                        break;
                    case SpawnerType.Ufo:
                        UfoSpawner ufoSpawner = new UfoSpawner();
                        ufoSpawner.Initialize(_playerShip, _mainCamera, spawnerSettings, objectPooler, _enemyDeathListener, _gameSessionData);
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
