using UnityEngine;
using Zenject.SpaceFighter;

namespace AsteroidGame
{

    public class AsteroidSpawner : EnemySpawner
    {
        protected override void Spawn()
        {
            Vector3 spawnPosition = GetPositionOutsideScreen();
            GameObject obstacle = _objectPooler.GetObject();
            obstacle.transform.position = spawnPosition;
            
            Enemy enemy = obstacle.GetComponent<Enemy>();
            enemy.Initialize(_objectPooler);
            enemy.OnKill += _enemyDeathListener.OnEnemyDeath;
            
            AsteroidMovement asteroidMovement = obstacle.GetComponent<AsteroidMovement>();
            asteroidMovement.SetStartDirection();
        }
    }
}