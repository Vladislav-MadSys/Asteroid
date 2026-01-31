using System;
using _Project.Scripts.Purchases;
using _Project.Scripts.Services;

namespace _Project.Scripts.Saves
{
    public class SaveDataConstructor
    {
        private GameSessionData _gameSessionData;
        private IPurchaser _purchaser;

        public void Initialize(GameSessionData gameSessionData, IPurchaser purchaser)
        {
            _gameSessionData = gameSessionData;
            _purchaser = purchaser;
        }

        public SaveData GetSaveData()
        {
            SaveData data = new SaveData();
            
            data.playerPosition = _gameSessionData.PlayerPosition;
            data.playerRotation = _gameSessionData.PlayerRotation;
            data.points = _gameSessionData.Points;
            data.isAdsRemoved = _purchaser.IsAdsRemoved;
            data.saveTime = DateTime.Now.ToString();
            
            return data;
        }
        
    }
}
