using System;
using _Project.Scripts.Saves;
using UnityEngine;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuUIModel : IDisposable
    {
        public event Action<SaveData, SaveData> OnConflictWithCloudSave;
        
        private ISaveSystem _saveSystem;
        
        public void Initialize(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            
            _saveSystem.OnConflictWithCloudSave += ShowPanelWithSelectingLocalSave;
            _saveSystem.Load();
        }

        public void Dispose()
        {
            _saveSystem.OnConflictWithCloudSave -= ShowPanelWithSelectingLocalSave;
        }

        private void ShowPanelWithSelectingLocalSave(SaveData localSaveData, SaveData cloudSaveData)
        {
            OnConflictWithCloudSave?.Invoke(localSaveData, cloudSaveData);
        }

        public void SelectSave(bool isCloud)
        {
            if (isCloud)
            {
                _saveSystem.UseCloudSave();
            }
            else
            {
                _saveSystem.UseLocalSave();
            }
        }
    }
}
