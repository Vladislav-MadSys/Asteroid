using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{

    public class AsteroidSpawner : EnemySpawner
    {
        protected override void Spawn()
        {
            Vector3 spawnPosition = GetPositionOutsideScreen();
            GameObject obstacle = _objectPooler.GetObject();
            obstacle.transform.position = spawnPosition;
            
            Enemy enemy = obstacle.GetComponent<Enemy>();
            enemy.SetDeathListener(_enemyDeathListener);
            enemy.OnKill += OnMyEnemyKill;
            
            AsteroidMovement asteroidMovement = obstacle.GetComponent<AsteroidMovement>();
            asteroidMovement.SetStartDirection();
        }
    }
}