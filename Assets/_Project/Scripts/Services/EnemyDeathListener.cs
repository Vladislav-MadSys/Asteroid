using AsteroidGame;
using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class EnemyDeathListener
    {
        private GameSessionData _gameSessionData;

        [Inject]
        private void Inject(GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
        }
        public void OnEnemyDeath(int delta)
        {
            
            _gameSessionData.ChangePoints(delta);
        }
    }
}
