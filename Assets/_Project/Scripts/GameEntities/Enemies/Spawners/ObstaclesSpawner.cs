using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AsteroidGame
{
    public class ObstaclesSpawner : ITickable
    {
        protected SpawnerSettings Settings;

        protected GameEvents GameEvents;
        protected ObjectPooler _objectPooler;
        protected Camera _mainCamera;
        protected EnemyDeathListener _enemyDeathListener;
        
        private float _timer;
       
        
        public void Initialize(
            GameEvents gameEvents,
            Camera mainCamera,
            SpawnerSettings settings,
            ObjectPooler objectPooler,
            EnemyDeathListener enemyDeathListener)
        {
            GameEvents = gameEvents;
            _mainCamera = mainCamera;
            Settings = settings;
            _objectPooler = objectPooler;
            _enemyDeathListener = enemyDeathListener;
        }
        
        public void Tick()
        {
            if (_timer >= Settings.TimeToSpawn)
            {
                _timer = 0;
                Spawn();
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        protected Vector3 GetPositionOutsideScreen()
        {
            Vector3 viewportPos = Vector3.zero;
            Vector3 worldPos = Vector3.zero;

            int side = Random.Range(0, 4); // Sides

            float randomOffset = Random.Range(0f, Settings.OffsetOutOfScreen);

            switch (side)
            {
                case 0: //left
                    viewportPos = new Vector3(-Settings.OffsetOutOfScreen - randomOffset, Random.Range(0f, 1f), 0f);
                    break;
                case 1: //right
                    viewportPos = new Vector3(1 + Settings.OffsetOutOfScreen + randomOffset, Random.Range(0f, 1f), 0f);
                    break;
                case 2: //up
                    viewportPos = new Vector3(Random.Range(0f, 1f), 1 + Settings.OffsetOutOfScreen + randomOffset, 0f);
                    break;
                case 3: //dawn
                    viewportPos = new Vector3(Random.Range(0f, 1f), -Settings.OffsetOutOfScreen - randomOffset, 0f);
                    break;
            }

            worldPos = _mainCamera.ViewportToWorldPoint(viewportPos);
            worldPos.z = 0f;

            return worldPos;
        }

        protected virtual void Spawn()
        {
            Vector3 spawnPosition = GetPositionOutsideScreen();
            GameObject obstacle = _objectPooler.GetObject();
            obstacle.transform.position = spawnPosition;
            if (obstacle.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Initialize(_objectPooler);
                enemy.OnKill += _enemyDeathListener.OnEnemyDeath;
            }

        }
    }
}
