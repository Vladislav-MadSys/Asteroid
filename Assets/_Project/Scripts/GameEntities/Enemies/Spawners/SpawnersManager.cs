using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class SpawnersManager : MonoBehaviour
    {
        [SerializeField] private SpawnerSettings[] Settings;
        
        protected PlayerStates PlayerStates;
        
        private Camera _mainCamera;
        private PlayerShip _playerShip;
        private float _timer;
        private EnemyDeathListener _enemyDeathListener;
        
        private List<EnemySpawner> _obstaclesSpawner = new List<EnemySpawner>();
        
        [Inject]
        private void Inject(PlayerShip playerShip, Camera mainCamera, EnemyDeathListener enemyDeathListener)
        {
            _playerShip = playerShip;
            _mainCamera = mainCamera;
            _enemyDeathListener = enemyDeathListener;
        }

        private void Awake()
        {
            foreach (SpawnerSettings spawnerSettings in Settings)
            {
                ObjectPooler objectPooler = new ObjectPooler(spawnerSettings.Prefab);
                objectPooler.Initialize();
                switch (spawnerSettings.Type)
                {
                    case SpawnerType.Asteroid:
                        AsteroidSpawner asteroidSpawner = new AsteroidSpawner();
                        asteroidSpawner.Initialize(_mainCamera,spawnerSettings, objectPooler, _enemyDeathListener);
                        _obstaclesSpawner.Add(asteroidSpawner);
                        break;
                    case SpawnerType.Ufo:
                        UfoSpawner ufoSpawner = new UfoSpawner();
                        ufoSpawner.Initialize(_playerShip, _mainCamera, spawnerSettings, objectPooler, _enemyDeathListener);
                        _obstaclesSpawner.Add(ufoSpawner);
                        break;
                }
            }
            
        }

        public void Update()
        { 
            foreach (var spawner in _obstaclesSpawner)
            {
                spawner.Tick();
            }
        }
    }
}
