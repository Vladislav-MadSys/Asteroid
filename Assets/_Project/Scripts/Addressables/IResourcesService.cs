using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Addressables
{
    public interface IResourcesService
    {
        public UniTask<GameObject> Load(string key);
        public void Unload(string key);
    }
}
