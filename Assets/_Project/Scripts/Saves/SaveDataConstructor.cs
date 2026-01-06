using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Saves
{
    public class SaveDataConstructor
    {
        [Inject] private GameSessionData _gameSessionData;

        
       /* private void Inject(GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
        }*/

        public SaveData GetSaveData()
        {
            SaveData data = new SaveData();
            
            data.playerPosition = _gameSessionData.PlayerPosition;
            data.playerRotation = _gameSessionData.PlayerRotation;
            data.points = _gameSessionData.Points;
            
            return data;
        }
        
    }
}
