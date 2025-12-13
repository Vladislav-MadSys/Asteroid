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
    
        public void SetDeathListener(EnemyDeathListener enemyDeathListener)
        {
            _enemyDeathListener = enemyDeathListener;
        }

        public virtual void Kill()
        {
            _enemyDeathListener.OnEnemyDeath(this);
            if (OnKill == null)
            {
                Destroy(gameObject);   
            }
            OnKill?.Invoke(this);
            
        }
    }
}