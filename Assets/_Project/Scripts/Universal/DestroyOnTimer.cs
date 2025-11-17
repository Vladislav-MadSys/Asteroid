using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 30;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
