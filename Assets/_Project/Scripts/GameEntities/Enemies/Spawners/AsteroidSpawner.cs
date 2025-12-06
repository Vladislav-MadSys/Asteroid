using UnityEngine;
using Zenject.SpaceFighter;

namespace AsteroidGame
{

    public class AsteroidSpawner : ObstaclesSpawner
    {
        protected override void Spawn()
        {
            Vector3 spawnPosition = GetPositionOutsideScreen();
            GameObject obstacle = _objectPooler.GetObject();
            obstacle.transform.position = spawnPosition;
            if (obstacle.TryGetComponent(out Enemy enemy))
            {
                enemy.Initialize(_objectPooler);
            }

            if (obstacle.TryGetComponent(out AsteroidMovement asteroidMovement))
            {
                asteroidMovement.SetStartDirection();
            }
            
        }
    }
}