using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class SaveSystem_PlayerPrefs : ISaveService
    {
        private SaveDataConstructor _saveDataConstructor;
        
        [Inject]
        private void Inject(SaveDataConstructor saveDataConstructor)
        {
            _saveDataConstructor = saveDataConstructor;
        }
        
        public void Save()
        {
            string save = JsonUtility.ToJson(_saveDataConstructor.GetSaveData());
            PlayerPrefs.SetString("basicSave", save);
        }

        public void Load()
        {
            if(!PlayerPrefs.HasKey("basicSave")) return;
            
            string save = PlayerPrefs.GetString("basicSave");
            Debug.Log(save);
        }
    }
}
