using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class UfoSpawner : ObstaclesSpawner
    {
        private PlayerShip _playerShip;

        public void Initialize(PlayerShip playerShip, GameEvents gameEvents, Camera mainCamera, SpawnerSettings settings, ObjectPooler objectPooler)
        {
            _playerShip = playerShip;
            _objectPooler = objectPooler;
            _mainCamera = mainCamera;
            Settings = settings;
            GameEvents = gameEvents;
        }

        protected override void Spawn()
        {
            Vector3 spawnPosition = GetPositionOutsideScreen();
            GameObject obstacle = _objectPooler.GetObject();
            obstacle.transform.position = spawnPosition;
            obstacle.transform.rotation = Quaternion.identity;
            if (obstacle.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Initialize(_objectPooler);
            }

            if (obstacle.TryGetComponent(out UfoMovement ufoMovment))
            {
                ufoMovment.Initialize(_playerShip);
            }
        }
    }
}
