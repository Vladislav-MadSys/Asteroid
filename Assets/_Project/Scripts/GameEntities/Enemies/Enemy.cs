using System;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnKill;
        
        [field: SerializeField] public int PointsForKill {get; private set; }
        
        protected EnemyDeathListener _enemyDeathListener;
        protected GameSessionData _gameSessionData;
        protected bool _isFromPool = false;
    
        public void Initialize(EnemyDeathListener enemyDeathListener, GameSessionData gameSessionData, bool isFromPool)
        {
            _enemyDeathListener = enemyDeathListener;
            _gameSessionData = gameSessionData;
            _isFromPool = isFromPool;
        }

        public virtual void Kill()
        {
            _enemyDeathListener.OnEnemyDeath(this);

            if (_isFromPool)
            {
                OnKill?.Invoke(this);
            }
            else
            {
                OnKill?.Invoke(this);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            OnKill = null;
        }
    }
}