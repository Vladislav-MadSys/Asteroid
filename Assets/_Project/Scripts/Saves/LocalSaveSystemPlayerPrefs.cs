using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class LocalSaveSystemPlayerPrefs : ILocalSaveService
    {
        private const string SAVE_KEY = "basicSave";
        
        private SaveDataConstructor _saveDataConstructor;
        private SaveData _cachedSaveData;

        public LocalSaveSystemPlayerPrefs(SaveDataConstructor saveDataConstructor)
        {
            _saveDataConstructor = saveDataConstructor;
        }
        
        public void Save()
        {
            string save = JsonUtility.ToJson(_saveDataConstructor.GetSaveData(_cachedSaveData));
            PlayerPrefs.SetString(SAVE_KEY, save);
        }

        public void Save(SaveData saveData)
        {
            string save = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(SAVE_KEY, save);
        }

        public SaveData Load()
        {
            if (!PlayerPrefs.HasKey(SAVE_KEY)) return null;
            
            _cachedSaveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVE_KEY));
            return _cachedSaveData;
        }
    }
}
