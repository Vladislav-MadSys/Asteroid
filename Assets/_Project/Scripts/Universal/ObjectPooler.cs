using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    
    private GameObject _prefab;
    private int _startPoolSize = 50;
    
    private Queue<GameObject> _objectPool = new Queue<GameObject>();


    public void Initialize(GameObject prefab, int startPoolSize = 50)
    {
        _prefab =  prefab;
        _startPoolSize = startPoolSize;
    }
    
    private void Start()
    {
        for (int i = 0; i < _startPoolSize; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
        
    }

    public GameObject GetObject()
    {
        if (_objectPool.Count > 0)
        {
            GameObject obj = _objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return Instantiate(_prefab);
    }

    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        _objectPool.Enqueue(gameObject);
    }
}
