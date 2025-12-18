using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies
{
    public class Asteroid : Enemy
    {
        [SerializeField] private GameObject[] _objectsToSpawnOnDeath;
        [SerializeField] private int _lifeTimeMs = 10000;
        
        private CancellationTokenSource _cancellationTokenSource;

        private async void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await UniTask.Delay(_lifeTimeMs, cancellationToken: _cancellationTokenSource.Token);
                Kill();
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }
        
        public override void Kill()
        {
            _cancellationTokenSource.Cancel();
            if (_objectsToSpawnOnDeath.Length > 0)
            {
                foreach (GameObject deathObjectPrefab in _objectsToSpawnOnDeath)
                {
                    GameObject deathObject = Instantiate(deathObjectPrefab, transform);
                    deathObject.transform.parent = transform.parent;

                    if (deathObject.TryGetComponent(out AsteroidMovement asteroidMovement))
                    {
                        asteroidMovement.SetStartDirection();
                    }

                    if (deathObject.TryGetComponent(out Enemy enemy))
                    {
                        enemy.Initialize(_enemyDeathListener, false);
                    }
                    
                }
            }

            base.Kill();
        }
        
    }
}