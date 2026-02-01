using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class SaveSystem : ISaveSystem
    {
        private const string SAVE_KEY = "basicSave";
    
        public event Action<SaveData> OnSaveLoaded;
        public event Action<SaveData, SaveData> OnConflictWithCloudSave;
    
        private ILocalSaveService _localSaveService;
        private ICloudSaveService _cloudSaveService;
        private SaveData _cachedLocalSaveData;
        private SaveData _cachedCloudSaveData;
        
        public SaveData CachedSaveData { get; private set; }
    
        public SaveSystem(ILocalSaveService localSaveService, ICloudSaveService cloudSaveService)
        {
            _localSaveService = localSaveService;
            _cloudSaveService = cloudSaveService;
        }
        
        public void Save()
        {
            _localSaveService.Save();
            _cloudSaveService.Save();
        }
    
        public async UniTask Load()
        {
            _cachedLocalSaveData = _localSaveService.Load();
            _cachedCloudSaveData = await _cloudSaveService.Load();

            if (_cachedLocalSaveData != null && _cachedCloudSaveData != null && _cachedLocalSaveData.saveTime !=_cachedCloudSaveData.saveTime)
            {
                OnConflictWithCloudSave?.Invoke(_cachedLocalSaveData, _cachedCloudSaveData);   
            }
            else if (_cachedCloudSaveData != null)
            {
                UseCloudSave();
            }
            else if (_cachedLocalSaveData != null)
            {
                UseLocalSave();
            }
        }
        
        public void UseCloudSave()
        {
            CachedSaveData = _cachedCloudSaveData;
            _localSaveService.Save(_cachedCloudSaveData);
            OnSaveLoaded?.Invoke(CachedSaveData);
        }
        
        public void UseLocalSave()
        {
            CachedSaveData = _cachedLocalSaveData;
            _cloudSaveService.Save(_cachedLocalSaveData);
            OnSaveLoaded?.Invoke(CachedSaveData);
        }
    }
}
