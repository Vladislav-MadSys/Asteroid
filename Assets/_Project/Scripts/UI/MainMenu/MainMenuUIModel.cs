using System;
using _Project.Scripts.Saves;
using UnityEngine;

namespace _Project.Scripts.UI.MainMenu
{
    public class MainMenuUIModel : IDisposable
    {
        public event Action<SaveData, SaveData> OnConflictWithCloudSave;
        
        private ISaveService _saveService;
        
        public void Initialize(ISaveService saveService)
        {
            _saveService = saveService;
            
            _saveService.OnConflictWithCloudSave += ShowPanelWithSelectingSave;
        }

        public void Dispose()
        {
            _saveService.OnConflictWithCloudSave -= ShowPanelWithSelectingSave;
        }

        private void ShowPanelWithSelectingSave(SaveData localSaveData, SaveData cloudSaveData)
        {
            OnConflictWithCloudSave?.Invoke(localSaveData, cloudSaveData);
        }

        public void SelectSave(bool isCloud)
        {
            if (isCloud)
            {
                _saveService.UseCloudSave();
            }
            else
            {
                _saveService.UseLocalSave();
            }
        }
    }
}
