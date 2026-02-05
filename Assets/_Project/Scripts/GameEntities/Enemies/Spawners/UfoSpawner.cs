using _Project.Scripts.Config;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class UfoSpawner : EnemySpawner
    {
        private PlayerFactory _playerFactory;
        private ConfigData _configData;

        public void Initialize(
            PlayerFactory playerFactory, 
            Camera mainCamera, 
            SpawnerSettings settings, 
            ObjectPool<Enemy> objectPool, 
            EnemyDeathListener enemyDeathListener, 
            GameSessionData gameSessionData,
            ConfigData configData)
        {
            _playerFactory = playerFactory;
            ObjectPool = objectPool;
            _mainCamera = mainCamera;
            Settings = settings;
            _enemyDeathListener = enemyDeathListener;
            _gameSessionData = gameSessionData;
            _configData = configData;
        }

        protected override Enemy Spawn()
        {
            Enemy obstacle = base.Spawn();
            
            UfoMovement ufoMovment = obstacle.GetComponent<UfoMovement>();
            ufoMovment.Initialize(_playerFactory, _configData, _gameSessionData);
            
            
            return obstacle;
        }
    }
}
