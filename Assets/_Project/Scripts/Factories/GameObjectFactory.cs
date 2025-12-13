using UnityEngine;

namespace _Project.Scripts.Factories
{
    public class GameObjectFactory : IFactory<GameObject>
    {
        private readonly GameObject _prefab;
        
        public GameObjectFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public GameObject Create()
        {
            return Object.Instantiate(_prefab);
        }

        public GameObject Create(Vector3 position, Quaternion rotation)
        {
            return Object.Instantiate(_prefab, position, rotation);
        }
        
    }
    
}
