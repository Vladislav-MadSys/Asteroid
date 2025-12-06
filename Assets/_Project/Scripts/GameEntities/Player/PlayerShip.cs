using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class PlayerShip : MonoBehaviour
    {
        private GameSessionData _gameSessionData;

        [Inject]
        private void Inject(GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
        }

        public void KillPlayer()
        {
            _gameSessionData.KillPlayer();
            Destroy(gameObject);
        }
    }
}
