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

        public SceneSaveController(SaveDataConstructor saveDataConstructor, GameSessionData gameSessionData,
            ISaveService saveService, IPurchaser purchaser)
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
            if (save != null)
            {
                OnSaveLoaded?.Invoke(save);
                _gameSessionData.PreviousPoints = save.points;
            }
        }
    }
}
