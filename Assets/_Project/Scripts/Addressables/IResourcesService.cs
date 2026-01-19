using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Addressables
{
    public interface IResourcesService
    {
        public UniTask<T> Load<T>(string key);
        public void Unload(string key);
        public void UnloadAll();
    }
}
