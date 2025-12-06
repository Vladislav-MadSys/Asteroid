using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class GameSessionData
    {
        [field: SerializeField] public int Points { get; private set; } = 0;

        private GameEvents _gameEvents;

        [Inject]
        private void Inject(GameEvents gameEvents)
        {
            _gameEvents = gameEvents;
            _gameEvents.OnEnemyKilled += ChangePoints;
        }

        private void ChangePoints(int deltaPoints)
        {
            Points += deltaPoints;
            _gameEvents.ChangePoints(Points);
        }
    }
}
