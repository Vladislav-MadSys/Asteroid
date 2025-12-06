using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class SpawnersManager : MonoBehaviour
    {
        [SerializeField] private SpawnerSettings[] Settings;
        
        protected GameEvents GameEvents;
        
        private Camera _mainCamera;
        private PlayerShip _playerShip;
        private float _timer;
        
        private List<ObstaclesSpawner> _obstaclesSpawner = new List<ObstaclesSpawner>();
        
        [Inject]
        private void Inject(PlayerShip playerShip, GameEvents gameEvents, Camera mainCamera)
        {
            _playerShip = playerShip;;
            GameEvents = gameEvents;
            _mainCamera = mainCamera;
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
                        asteroidSpawner.Initialize(GameEvents, _mainCamera,spawnerSettings, objectPooler);
                        _obstaclesSpawner.Add(asteroidSpawner);
                        break;
                    case SpawnerType.Ufo:
                        UfoSpawner ufoSpawner = new UfoSpawner();
                        ufoSpawner.Initialize(_playerShip, GameEvents, _mainCamera, spawnerSettings, objectPooler);
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
