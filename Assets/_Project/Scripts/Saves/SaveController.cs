using System;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class SaveController : IInitializable, IDisposable
    {
        private ISaveService _saveService;
        private GameSessionData _gameSessionData;

        [Inject]
        private void Inject(ISaveService saveService, GameSessionData gameSessionData)
        {
            _saveService = saveService;
            _gameSessionData = gameSessionData;
        }

        public void Initialize()
        {
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
            _saveService.Load();
        }
    }
}
