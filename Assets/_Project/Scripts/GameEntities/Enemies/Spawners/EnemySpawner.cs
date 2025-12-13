using System;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class EnemySpawner : ITickable
    {
        private const float VIEWPORT_SCALE = 1f;
        
        protected SpawnerSettings Settings;

        protected ObjectPooler _objectPooler;
        protected Camera _mainCamera;
        protected EnemyDeathListener _enemyDeathListener;
        
        private float _timer;
       
        
        public void Initialize(
            Camera mainCamera,
            SpawnerSettings settings,
            ObjectPooler objectPooler,
            EnemyDeathListener enemyDeathListener)
        {
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

            int sidesCount = Enum.GetValues(typeof(ScreenSide)).Length;
            ScreenSide side = (ScreenSide)Random.Range(0,  sidesCount);

            float randomOffset = Random.Range(0f, Settings.OffsetOutOfScreen);

            switch (side)
            {
                case ScreenSide.Left:
                    viewportPos = new Vector3(
                        -Settings.OffsetOutOfScreen - randomOffset,
                        Random.Range(0f, VIEWPORT_SCALE),
                        0f);
                    break;
                case ScreenSide.Right:
                    viewportPos = new Vector3(
                        VIEWPORT_SCALE + Settings.OffsetOutOfScreen + randomOffset,
                        Random.Range(0f, VIEWPORT_SCALE),
                        0f);
                    break;
                case ScreenSide.Top:
                    viewportPos = new Vector3(
                        Random.Range(0f, VIEWPORT_SCALE),
                        VIEWPORT_SCALE + Settings.OffsetOutOfScreen + randomOffset,
                        0f);
                    break;
                case ScreenSide.Bottom:
                    viewportPos = new Vector3(
                        Random.Range(0f, VIEWPORT_SCALE),
                        -Settings.OffsetOutOfScreen - randomOffset,
                        0f);
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
            
            Enemy enemy = obstacle.GetComponent<Enemy>();
            enemy.SetDeathListener(_enemyDeathListener);
            enemy.OnKill += OnMyEnemyKill;
        }

        protected virtual void OnMyEnemyKill(Enemy enemy)
        {
            enemy.OnKill -= OnMyEnemyKill;
            _objectPooler.ReturnObject(enemy.gameObject);
        }
    }
}
