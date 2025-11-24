using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _pointsForKill;

    [SerializeField] private GameObject[] _objectsToSpawnOnDeath;

    public void Kill()
    {
        if(_objectsToSpawnOnDeath.Length > 0)
        {
            foreach(GameObject deathObjectPrefab in _objectsToSpawnOnDeath)
            {
                GameObject deathObject = Instantiate(deathObjectPrefab, transform);
                deathObject.transform.parent = transform.parent;
            }
        }
        GameEvents.KillEnemy(_pointsForKill);
        Destroy(gameObject);
    }
}
