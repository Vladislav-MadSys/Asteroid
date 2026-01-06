using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class SaveSystemPlayerPrefs : ISaveService
    {
        private const string SAVE_KEY = "basicSave";
        
        private SaveDataConstructor _saveDataConstructor;
        
        [Inject]
        private void Inject(SaveDataConstructor saveDataConstructor)
        {
            _saveDataConstructor = saveDataConstructor;
        }
        
        public void Save()
        {
            string save = JsonUtility.ToJson(_saveDataConstructor.GetSaveData());
            PlayerPrefs.SetString(SAVE_KEY, save);
        }

        public SaveData Load()
        {
            if(!PlayerPrefs.HasKey(SAVE_KEY)) return null;
            
            string save = PlayerPrefs.GetString(SAVE_KEY);
            Debug.Log(save);
            return JsonUtility.FromJson<SaveData>(save);
        }
    }
}
