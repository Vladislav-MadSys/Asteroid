using _Project.Scripts.GameEntities.Enemies;
using UnityEngine;
using System;
using Zenject;

namespace _Project.Scripts.Services
{
    public class EnemyDeathListener
    {
        public event Action<Vector3> OnEnemyDeath;
        
        private GameSessionData _gameSessionData;

        public EnemyDeathListener(GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
        }
        public void EnemyDeath(Enemy enemy)
        {
            _gameSessionData.ChangePoints(enemy.PointsForKill);
            OnEnemyDeath?.Invoke(enemy.transform.position);
        }
    }
}
