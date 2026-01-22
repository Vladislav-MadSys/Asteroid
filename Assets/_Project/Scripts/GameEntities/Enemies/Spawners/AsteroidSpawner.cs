using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class AsteroidSpawner : EnemySpawner
    {
        protected override Enemy Spawn()
        {
            Enemy obstacle = base.Spawn();
            
            AsteroidMovement asteroidMovement = obstacle.GetComponent<AsteroidMovement>();
            asteroidMovement.Initialize(_configData);
            asteroidMovement.SetStartDirection();
            
            return obstacle;
        }
    }
}