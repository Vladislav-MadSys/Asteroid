using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
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

        protected override GameObject Spawn()
        {
            GameObject obstacle = base.Spawn();
            
            UfoMovement ufoMovment = obstacle.GetComponent<UfoMovement>();
            ufoMovment.Initialize(_playerShip);
            
            return obstacle;
        }
    }
}
