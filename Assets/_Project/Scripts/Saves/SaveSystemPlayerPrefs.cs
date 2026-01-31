using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class SaveSystemPlayerPrefs : ISaveService, IInitializable
    {
        private const string SAVE_KEY = "basicSave";
        
        public event Action<SaveData> OnSaveLoaded;
        public event Action<SaveData, SaveData> OnConflictWithCloudSave;
        
        private SaveDataConstructor _saveDataConstructor;
        private ICloudSaveService _cloudSaveService; 
        
        private SaveData _cachedCloudSaveData;
        
        public SaveData CachedSaveData { get; private set; }
        public bool HasConflictWithCloudSave { get; private set; }

        public SaveSystemPlayerPrefs(SaveDataConstructor saveDataConstructor, ICloudSaveService cloudSaveService)
        {
            _saveDataConstructor = saveDataConstructor;
            _cloudSaveService = cloudSaveService;
        }

        public async void Initialize()
        {
            await Load();
        }
        public void Save()
        {
            string save = JsonUtility.ToJson(_saveDataConstructor.GetSaveData());
            PlayerPrefs.SetString(SAVE_KEY, save);
            _cloudSaveService.Save(save);
        }

        public async UniTask Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY))
            {
                CachedSaveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVE_KEY));
            }

            await LoadCloudSave();
        }

        public void UseCloudSave()
        {
            CachedSaveData = _cachedCloudSaveData;
            string save = JsonUtility.ToJson(CachedSaveData);
            PlayerPrefs.SetString(SAVE_KEY, save);
            OnSaveLoaded?.Invoke(CachedSaveData);
        }

        public void UseLocalSave()
        {
            OnSaveLoaded?.Invoke(CachedSaveData);
        }

        private async UniTask LoadCloudSave()
        {
                _cachedCloudSaveData = await _cloudSaveService.Load();
                if (CachedSaveData != null && _cachedCloudSaveData != null && CachedSaveData.saveTime != _cachedCloudSaveData.saveTime)
                {
                    HasConflictWithCloudSave = true;
                    OnConflictWithCloudSave?.Invoke(CachedSaveData, _cachedCloudSaveData);
                }
                else if (_cachedCloudSaveData != null)
                {
                    HasConflictWithCloudSave = false;
                    CachedSaveData = _cachedCloudSaveData;
                }
        }
        
    }
}
