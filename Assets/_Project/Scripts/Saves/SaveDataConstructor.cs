using System;
using _Project.Scripts.Purchases;
using _Project.Scripts.Services;
using UnityEngine;

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

        public SaveData GetSaveData(SaveData currentSaveData)
        {
            SaveData data = new SaveData();

            if (_gameSessionData != null)
            {
                data.playerPosition = _gameSessionData.PlayerPosition;
                data.playerRotation = _gameSessionData.PlayerRotation;
                data.points = _gameSessionData.Points;
            }
            else if(currentSaveData != null)
            {
                data.playerPosition = currentSaveData.playerPosition;
                data.playerRotation = currentSaveData.playerRotation;
                data.points = currentSaveData.points;
            }
            else
            {
                data.playerPosition = Vector3.zero;
                data.playerRotation = 0;
                data.points = 0;
            }

            if (_purchaser != null)
            {
                data.isAdsRemoved = _purchaser.IsAdsRemoved;
            }
            else if(currentSaveData != null)
            {
                data.isAdsRemoved = currentSaveData.isAdsRemoved;
            }
            else
            {
                data.isAdsRemoved = false;
            }

            data.saveTime = DateTime.Now.ToString();
            
            return data;
        }
        
    }
}
