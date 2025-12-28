using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class GameSessionData
    {
        public event Action OnPlayerKilled;
        public event Action OnPointsChanged;

        public Vector2 PlayerPosition { get; private set; }
        public float PlayerRotation { get; private set; }
        public int Points { get; private set; } = 0;
        
        public void ChangePoints(int deltaPoints)
        {
            Points += deltaPoints;
            OnPointsChanged?.Invoke();
        }

        public void ChangePlayerPosition(Vector2 newPlayerPosition)
        {
            PlayerPosition = newPlayerPosition;
        }

        public void ChangePlayerRotation(float newPlayerRotation)
        {
            PlayerRotation = newPlayerRotation;
        }

        public void KillPlayer()
        {
            OnPlayerKilled?.Invoke();   
        }
    }
}
