using System;
using _Project.Scripts.Addressables;
using _Project.Scripts.Config;
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
        protected IResourcesService _resourcesService;
        protected  ConfigData _config;
        protected bool _isFromPool = false;
    
        public virtual void Initialize(
            EnemyDeathListener enemyDeathListener, 
            GameSessionData gameSessionData, 
            IResourcesService resourcesService, 
            ConfigData configData,
            bool isFromPool)
        {
            _enemyDeathListener = enemyDeathListener;
            _gameSessionData = gameSessionData;
            _resourcesService = resourcesService;
            _config = configData;
            _isFromPool = isFromPool;
        }

        public virtual void Kill()
        {
            _enemyDeathListener.EnemyDeath(this);

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