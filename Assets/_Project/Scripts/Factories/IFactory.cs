using UnityEngine;

namespace AsteroidGame
{
    public interface IFactory<T> where T : UnityEngine.Object
    {
        T Create();
        T Create(Vector3 position, Quaternion rotation);
    }
}
