using _Project.Scripts.Services;

namespace _Project.Scripts.Saves
{
    public class SaveDataConstructor
    {
        private GameSessionData _gameSessionData;

        public void Initialize(GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
        }
        

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
