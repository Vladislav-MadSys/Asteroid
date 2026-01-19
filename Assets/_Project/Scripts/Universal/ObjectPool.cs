using System.Collections.Generic;
using _Project.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Universal
{

    public class ObjectPool<T> : IInitializable
    {
        private GameObjectFactory _factory;
        private GameObject _prefab;
        private int _startPoolSize = 50;

        private Queue<T> _objectPool = new Queue<T>();


        public ObjectPool(GameObject prefab, int startPoolSize = 50)
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
                
                T component = obj.GetComponent<T>();
                if (component != null)
                {
                    _objectPool.Enqueue(component);
                }
                else
                {
                    Debug.LogError($"Prefab {_prefab.name} does not contain a component of type {typeof(T)}");
                }
            }

        }

        public T GetObject()
        {
            if (_objectPool.Count > 0)
            {
                T component = _objectPool.Dequeue();
                return component;
            }
            
            GameObject newObj = _factory.Create();
            T newComponent = newObj.GetComponent<T>();
            return newComponent;
        }

        public void ReturnObject(T component)
        {
            _objectPool.Enqueue(component);
        }
    }
}
