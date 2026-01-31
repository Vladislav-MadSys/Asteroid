using System;
using _Project.Scripts.Purchases;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class SceneSaveController : IInitializable, IDisposable
    {
        public event Action<SaveData> OnSaveLoaded;
        
        private SaveDataConstructor _saveDataConstructor;
        private GameSessionData _gameSessionData;
        private ISaveService _saveService;
        private IPurchaser _purchaser;

        public SceneSaveController(
            SaveDataConstructor saveDataConstructor,
            GameSessionData gameSessionData,
            ISaveService saveService,
            IPurchaser purchaser)
        {
            _saveDataConstructor = saveDataConstructor;
            _gameSessionData = gameSessionData;
            _saveService = saveService;
            _purchaser = purchaser;
        }

        public void Initialize()
        {
            _saveDataConstructor.Initialize(_gameSessionData, _purchaser);
            _gameSessionData.OnPlayerKilled += SaveData;
            _saveService.OnSaveLoaded += OnSaveDataLoaded;

            LoadData();
        }

        public void Dispose()
        {
            _gameSessionData.OnPlayerKilled -= SaveData;
            _saveService.OnSaveLoaded -= OnSaveDataLoaded;
        }

        private void SaveData()
        {
            _saveService.Save();
        }

        private async void LoadData()
        {
            await _saveService.Load();
            SaveData save = _saveService.CachedSaveData;
            if (save != null)
            {
                if (!_saveService.HasConflictWithCloudSave)
                {
                    OnSaveDataLoaded(save);
                }
            }
        }
        
        private void OnSaveDataLoaded(SaveData save)
        {
            OnSaveLoaded?.Invoke(save);
            _gameSessionData.PreviousPoints = save.points;
        }
    }
}
