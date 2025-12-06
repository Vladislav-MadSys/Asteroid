using System;
using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class GameSessionData
    {
        public event Action OnPointsChanged;
        
        public int Points { get; private set; } = 0;

        public void ChangePoints(int deltaPoints)
        {
            Points += deltaPoints;
            OnPointsChanged?.Invoke();
        }
    }
}
