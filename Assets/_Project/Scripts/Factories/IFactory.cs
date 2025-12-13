using UnityEngine;

namespace _Project.Scripts.Factories
{
    public interface IFactory<T> where T : UnityEngine.Object
    {
        T Create();
        T Create(Vector3 position, Quaternion rotation);
    }
}
