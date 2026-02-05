using System;
using _Project.Scripts.Analytics;
using _Project.Scripts.Saves;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class GameSessionData : IInitializable, IDisposable
    {
        public event Action OnPlayerKilled;
        public event Action OnPlayerRespawned;
        public event Action OnPointsChanged;

        public Vector2 PlayerPosition { get; private set; }
        public float PlayerRotation { get; private set; }
        public int Points { get; private set; } = 0;
        public int PreviousPoints = 0;
        
        public int ShotsCount { get; private set; } = 0;
        public int LaserUsesCount { get; private set; } = 0;
        public int DestroyedAsteroids { get; private set; } = 0;
        public int DestroyedUfos { get; private set; } = 0;

        private PlayerStates _playerState;
        private IAnalyticsService _analyticsService;
        
        
        public GameSessionData(PlayerStates playerState, IAnalyticsService analyticsService)
        {
            _playerState = playerState;
            _analyticsService = analyticsService;
        }

        public void Initialize()
        {
            _playerState.OnPlayerPositionChanged += ChangePlayerPosition;
            _playerState.OnPlayerRotationChanged += ChangePlayerRotation;
            
            _analyticsService.LogGameStart();
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
            _analyticsService.LogGameEnd(ShotsCount, LaserUsesCount, DestroyedAsteroids, DestroyedUfos);
        }

        public void RespawnPlayer()
        {
            OnPlayerRespawned?.Invoke();
        }

        public void AddShot()
        {
            ShotsCount++;
        }

        public void AddLaserUses()
        {
            LaserUsesCount++;
            _analyticsService.LogLaserUse();
        }
        
        public void AddDestroyedAsteroids()
        {
            DestroyedAsteroids++;
        }

        public void AddDestroyedUfos()
        {
            DestroyedUfos++;
        }
        
    }
}
