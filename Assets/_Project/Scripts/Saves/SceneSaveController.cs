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
        private ISaveSystem _saveSystem;
        private IPurchaser _purchaser;

        public SceneSaveController(
            SaveDataConstructor saveDataConstructor,
            GameSessionData gameSessionData,
            ISaveSystem saveSystem,
            IPurchaser purchaser)
        {
            _saveDataConstructor = saveDataConstructor;
            _gameSessionData = gameSessionData;
            _saveSystem = saveSystem;
            _purchaser = purchaser;
        }

        public void Initialize()
        {
            _saveDataConstructor.Initialize(_gameSessionData, _purchaser);
            _gameSessionData.OnPlayerKilled += SaveData;
            _saveSystem.OnSaveLoaded += OnLocalSaveDataLoaded;

            LoadData();
        }

        public void Dispose()
        {
            _gameSessionData.OnPlayerKilled -= SaveData;
            _saveSystem.OnSaveLoaded -= OnLocalSaveDataLoaded;
        }

        private void SaveData()
        {
            _saveSystem.Save();
        }

        private async void LoadData()
        {
            await _saveSystem.Load();
        }
        
        private void OnLocalSaveDataLoaded(SaveData save)
        {
            OnSaveLoaded?.Invoke(save);
            _gameSessionData.PreviousPoints = save.points;
        }
    }
}
