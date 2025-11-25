using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _pointsForKill;
    [SerializeField] private GameObject[] _objectsToSpawnOnDeath;

    private GameEvents _gameEvents;

    public void Initialize(GameEvents gameEvents)
    {
        _gameEvents = gameEvents;
    }
    
    public void Kill()
    {
        if(_objectsToSpawnOnDeath.Length > 0)
        {
            foreach(GameObject deathObjectPrefab in _objectsToSpawnOnDeath)
            {
                GameObject deathObject = Instantiate(deathObjectPrefab, transform);
                deathObject.transform.parent = transform.parent;
                if (deathObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.Initialize(_gameEvents);
                }
                
            }
        }
        _gameEvents.KillEnemy(_pointsForKill);
        Destroy(gameObject);
    }
}
