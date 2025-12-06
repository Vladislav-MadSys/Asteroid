using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject Prefab;
    [SerializeField] protected float TimeToSpawn = 1;
    [SerializeField] protected float offsetOutOfScreen = 0.1f;
    
    protected GameEvents GameEvents;
    
    private Camera _mainCamera;
    private float _timer;

    [Inject]
    private void Inject(GameEvents gameEvents)
    {
        GameEvents = gameEvents;
    }
    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if(_timer >= TimeToSpawn)
        {
            _timer = 0;
            Spawn();
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    protected Vector3 GetPositionOutsideScreen()
    {
        Vector3 viewportPos = Vector3.zero;
        Vector3 worldPos = Vector3.zero;

        int side = Random.Range(0, 4); // Sides

        float randomOffset = Random.Range(0f, offsetOutOfScreen);

        switch (side)
        {
            case 0: //left
                viewportPos = new Vector3(-offsetOutOfScreen - randomOffset, Random.Range(0f, 1f), 0f);
                break;
            case 1: //right
                viewportPos = new Vector3(1 + offsetOutOfScreen + randomOffset, Random.Range(0f, 1f), 0f);
                break;
            case 2: //up
                viewportPos = new Vector3(Random.Range(0f, 1f), 1 + offsetOutOfScreen + randomOffset, 0f);
                break;
            case 3: //dawn
                viewportPos = new Vector3(Random.Range(0f, 1f), -offsetOutOfScreen - randomOffset, 0f);
                break;
        }

        worldPos = _mainCamera.ViewportToWorldPoint(viewportPos);
        worldPos.z = 0f; 

        return worldPos;
    }

    protected virtual void Spawn()
    {
        Vector3 spawnPosition = GetPositionOutsideScreen();
        GameObject obstacle = Instantiate(Prefab, spawnPosition, Quaternion.identity);
        if (obstacle.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Initialize(GameEvents);
        }
        
    }
}
