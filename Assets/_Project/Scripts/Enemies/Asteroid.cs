using UnityEngine;

namespace AsteroidGame
{
    public class Asteroid : Enemy
    {
        [SerializeField] private GameObject[] _objectsToSpawnOnDeath;

        public override void Kill()
        {
            if (_objectsToSpawnOnDeath.Length > 0)
            {
                foreach (GameObject deathObjectPrefab in _objectsToSpawnOnDeath)
                {
                    GameObject deathObject = Instantiate(deathObjectPrefab, transform);
                    deathObject.transform.parent = transform.parent;
                    if (deathObject.TryGetComponent(out Enemy enemy))
                    {
                        enemy.Initialize(_gameEvents);
                    }

                }
            }

            base.Kill();
        }
    }
}