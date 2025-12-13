using System;

namespace _Project.Scripts.Services
{
    public class GameSessionData
    {
        public event Action OnPlayerKilled;
        public event Action OnPointsChanged;
        
        public int Points { get; private set; } = 0;

        public void ChangePoints(int deltaPoints)
        {
            Points += deltaPoints;
            OnPointsChanged?.Invoke();
        }

        public void KillPlayer()
        {
            OnPlayerKilled?.Invoke();   
        }
    }
}
