using System;
using _Project.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class GameSessionData : IInitializable, IDisposable
    {
        public event Action OnPlayerKilled;
        public event Action OnPointsChanged;

        public Vector2 PlayerPosition { get; private set; }
        public float PlayerRotation { get; private set; }
        public int Points { get; private set; } = 0;
        public int PreviousPoints { get; private set; } = 0;

        private PlayerStates _playerState;
        
        [Inject]
        private void Inject(PlayerStates playerState, SceneSaveController sceneSaveController)
        {
            _playerState = playerState;
        }

        public void Initialize()
        {
            _playerState.OnPlayerPositionChanged += ChangePlayerPosition;
            _playerState.OnPlayerRotationChanged += ChangePlayerRotation;
        }

        public void Dispose()
        {
            _playerState.OnPlayerPositionChanged -= ChangePlayerPosition;
            _playerState.OnPlayerRotationChanged -= ChangePlayerRotation;
        }
        
        public void ChangePoints(int deltaPoints)
        {
            Points += deltaPoints;
            OnPointsChanged?.Invoke();
        }
        
        public void SetPreviousPoints(int deltaPoints)
        {
            PreviousPoints = deltaPoints;
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
