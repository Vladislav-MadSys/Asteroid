using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies.Spawners
{
    public class AsteroidSpawner : EnemySpawner
    {
        protected override GameObject Spawn()
        {
            GameObject obstacle = base.Spawn();
            
            AsteroidMovement asteroidMovement = obstacle.GetComponent<AsteroidMovement>();
            asteroidMovement.SetStartDirection();
            
            return obstacle;
        }
    }
}