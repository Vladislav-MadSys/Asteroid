using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts.Addressables
{
    public class ResourceLoader : IResourcesService, IDisposable
    {
        private Dictionary<string, AsyncOperationHandle> _cachedHandles = new Dictionary<string, AsyncOperationHandle>();
    
        public async UniTask<GameObject> Load(string key)
        {
            if (_cachedHandles.ContainsKey(key))
            {
                return (GameObject)_cachedHandles[key].Result;
            }
        
            var handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>(key);
            await handle.Task;
            _cachedHandles[key] = handle;
            return handle.Result;
        }

        public async void Unload(string key)
        {
            if (!_cachedHandles.ContainsKey(key)) return;
        
            AsyncOperationHandle handle = _cachedHandles[key];
            UnityEngine.AddressableAssets.Addressables.Release(handle);
            _cachedHandles.Remove(key);
        }

        public void Dispose()
        {
            List<string> keysToClear = new List<string>(_cachedHandles.Keys);
            foreach (var key in keysToClear)
            {
                Unload(key);
            }
            _cachedHandles.Clear();
        }
    }
}
