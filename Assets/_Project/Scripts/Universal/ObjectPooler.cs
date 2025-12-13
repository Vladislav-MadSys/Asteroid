using System.Collections.Generic;
using _Project.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Universal
{

    public class ObjectPooler : IInitializable
    {
        private GameObjectFactory _factory;
        private GameObject _prefab;
        private int _startPoolSize = 50;

        private Queue<GameObject> _objectPool = new Queue<GameObject>();


        public ObjectPooler(GameObject prefab, int startPoolSize = 50)
        {
            _prefab = prefab;
            _startPoolSize = startPoolSize;
        }

        public void Initialize()
        {
            _factory = new GameObjectFactory(_prefab);
            for (int i = 0; i < _startPoolSize; i++)
            {
                GameObject obj = _factory.Create();
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

            return _factory.Create();
        }

        public void ReturnObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
            _objectPool.Enqueue(gameObject);
        }
    }
}
