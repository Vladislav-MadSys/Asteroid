using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public int pointsForKill { get; private set; }

    [SerializeField] private GameObject[] objectsToSpawnOnDeath;

    public void Kill()
    {
        if(objectsToSpawnOnDeath.Length > 0)
        {
            foreach(GameObject deathObjectPrefab in objectsToSpawnOnDeath)
            {
                GameObject deathObject = Instantiate(deathObjectPrefab, transform);
                deathObject.transform.parent = transform.parent;
            }
        }
        GameEvents.KillEnemy(pointsForKill);
        Destroy(gameObject);
    }
}
