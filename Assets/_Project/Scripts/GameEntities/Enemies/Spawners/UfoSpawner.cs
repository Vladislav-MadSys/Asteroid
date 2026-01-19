using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class UfoSpawner : EnemySpawner
    {
        private PlayerShip _playerShip;

        public void Initialize(PlayerShip playerShip, Camera mainCamera, SpawnerSettings settings, ObjectPool<Enemy> objectPool, EnemyDeathListener enemyDeathListener, GameSessionData gameSessionData)
        {
            _playerShip = playerShip;
            ObjectPool = objectPool;
            _mainCamera = mainCamera;
            Settings = settings;
            _enemyDeathListener = enemyDeathListener;
            _gameSessionData = gameSessionData;
        }

        protected override Enemy Spawn()
        {
            Enemy obstacle = base.Spawn();
            
            UfoMovement ufoMovment = obstacle.GetComponent<UfoMovement>();
            ufoMovment.Initialize(_playerShip);
            
            return obstacle;
        }
    }
}
