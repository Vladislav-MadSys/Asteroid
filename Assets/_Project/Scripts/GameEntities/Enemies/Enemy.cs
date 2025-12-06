using UnityEngine;

namespace AsteroidGame
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _pointsForKill;

        protected GameEvents _gameEvents;

        public void Initialize(GameEvents gameEvents)
        {
            _gameEvents = gameEvents;
        }

        public virtual void Kill()
        {
            _gameEvents.KillEnemy(_pointsForKill);
            Destroy(gameObject);
        }
    }
}