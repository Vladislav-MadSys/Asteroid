using Unity.VisualScripting;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    
    private Camera _mainCamera;

    [SerializeField] private GameObject _prefab;

    [SerializeField] private float _timeToSpawn = 1;
    private float _timer;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if(_timer >= _timeToSpawn)
        {
            _timer = 0;
            Spawn();
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    Vector3 GetPositionOutsideScreen()
    {
        Vector3 viewportPos = Vector3.zero;
        Vector3 worldPos = Vector3.zero;

        int side = Random.Range(0, 4);

        float randomOffset = Random.Range(0f, 0.1f);

        switch (side)
        {
            case 0: //left
                viewportPos = new Vector3(-0.1f - randomOffset, Random.Range(0f, 1f), 0f);
                break;
            case 1: //right
                viewportPos = new Vector3(1.1f + randomOffset, Random.Range(0f, 1f), 0f);
                break;
            case 2: //up
                viewportPos = new Vector3(Random.Range(0f, 1f), 1.1f + randomOffset, 0f);
                break;
            case 3: //dawn
                viewportPos = new Vector3(Random.Range(0f, 1f), -0.1f - randomOffset, 0f);
                break;
        }

        worldPos = _mainCamera.ViewportToWorldPoint(viewportPos);
        worldPos.z = 0f; 

        return worldPos;
    }

    void Spawn()
    {
        Vector3 spawnPosition = GetPositionOutsideScreen();
        GameObject obstacle = Instantiate(_prefab, spawnPosition, Quaternion.identity);
    }
}
