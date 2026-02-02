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
    
        public void Dispose()
        {
            UnloadAll();
        }
        
        public async UniTask<T> Load<T>(string key)
        {
            AsyncOperationHandle handle = GetHandle<T>(key);
            
            await handle.Task;
            _cachedHandles[key] = handle;
            if (handle.Result is GameObject gameObject && typeof(T) != typeof(GameObject))
            {
                return gameObject.GetComponent<T>();
            }
            else
            {
                return (T)handle.Result;
            }
        }

        public async void Unload(string key)
        {
            if (!_cachedHandles.ContainsKey(key)) return;
        
            AsyncOperationHandle handle = _cachedHandles[key];
            UnityEngine.AddressableAssets.Addressables.Release(handle);
            _cachedHandles.Remove(key);
        }

        

        public void UnloadAll()
        {
            List<string> keysToClear = new List<string>(_cachedHandles.Keys);
            foreach (var key in keysToClear)
            {
                Unload(key);
            }
            _cachedHandles.Clear();
        }

        private AsyncOperationHandle GetHandle<T>(string key)
        {
            
            bool isComponent = typeof(Component).IsAssignableFrom(typeof(T));


            if (!_cachedHandles.ContainsKey(key))
            {
                if (isComponent && typeof(T) != typeof(GameObject))
                {
                    return UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>(key);
                }
                else
                {
                    return UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<T>(key);
                }
            }
            else
            {
                return _cachedHandles[key];
            }
        }
    }
}
