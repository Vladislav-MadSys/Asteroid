using System;
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

        [Inject]
        private void Inject(SaveDataConstructor saveDataConstructor, GameSessionData gameSessionData,
            ISaveService saveService)
        {
            _saveDataConstructor = saveDataConstructor;
            _gameSessionData = gameSessionData;
            _saveService = saveService;
        }

        public void Initialize()
        {
            _saveDataConstructor.Initialize(_gameSessionData);
            _gameSessionData.OnPlayerKilled += SaveData;

            LoadData();
        }

        public void Dispose()
        {
            _gameSessionData.OnPlayerKilled -= SaveData;
        }

        private void SaveData()
        {
            _saveService.Save();
        }

        private void LoadData()
        {
            SaveData save = _saveService.Load();
            OnSaveLoaded?.Invoke(save);
            _gameSessionData.SetPreviousPoints(save.points);
        }
    }
}
