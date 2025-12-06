using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class UfoSpawner : EnemySpawner
    {
        private PlayerShip _playerShip;

        public void Initialize(PlayerShip playerShip, Camera mainCamera, SpawnerSettings settings, ObjectPooler objectPooler, EnemyDeathListener enemyDeathListener)
        {
            _playerShip = playerShip;
            _objectPooler = objectPooler;
            _mainCamera = mainCamera;
            Settings = settings;
            _enemyDeathListener = enemyDeathListener;
        }

        protected override void Spawn()
        {
            Vector3 spawnPosition = GetPositionOutsideScreen();
            GameObject obstacle = _objectPooler.GetObject();
            obstacle.transform.position = spawnPosition;
            
            Enemy enemy = obstacle.GetComponent<Enemy>();
            enemy.Initialize(_objectPooler);
            enemy.OnKill += _enemyDeathListener.OnEnemyDeath;
            
            UfoMovement ufoMovment = obstacle.GetComponent<UfoMovement>();
            ufoMovment.Initialize(_playerShip);
        }
    }
}
