using System;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class PlayerStatsHudModel : IDisposable
    {
        public event Action<int> OnPointsChanged;
        public event Action<Vector2> OnPlayerPositionChanged;
        public event Action<float> OnPlayerRotationChanged;
        public event Action<int> OnLaserChargesChanged;
        public event Action<float> OnLaserTimeChanged;
        public event Action<int, int> OnPlayerKilled;
        

        public bool wasPlayerRespawned = false;

        private PlayerStates _playerStates;
        private GameSessionData _gameSessionData;

        public int Points => _gameSessionData.Points;

        public void Initialize(PlayerStates playerStates, GameSessionData gameSessionData)
        {
            _playerStates = playerStates;
            _gameSessionData = gameSessionData;
            
            _gameSessionData.OnPointsChanged += ChangePointsText;
            _playerStates.OnPlayerPositionChanged += ChangePlayerCoordinatesText;
            _playerStates.OnPlayerRotationChanged += ChangePlayerAngleText;
            _playerStates.OnLaserChargesChanged += ChangeLaserChargeText;
            _playerStates.OnLaserTimeChangedChanged += ChangeLaserReloadText;

            _gameSessionData.OnPlayerKilled += ShowLosePanel;
        }

        public void Dispose()
        {
            _gameSessionData.OnPointsChanged -= ChangePointsText;
            _playerStates.OnPlayerPositionChanged -= ChangePlayerCoordinatesText;
            _playerStates.OnPlayerRotationChanged -= ChangePlayerAngleText;
            _playerStates.OnLaserChargesChanged -= ChangeLaserChargeText;
            _playerStates.OnLaserTimeChangedChanged -= ChangeLaserReloadText;

            _gameSessionData.OnPlayerKilled -= ShowLosePanel;
            
        }

        private void ChangePointsText()
        {
            OnPointsChanged?.Invoke(Points);
        }

        private void ChangePlayerCoordinatesText(Vector2 newCoordinates)
        {
            OnPlayerPositionChanged?.Invoke(newCoordinates);
        }

        private void ChangePlayerAngleText(float newAngle)
        {
            OnPlayerRotationChanged?.Invoke(newAngle);
        }

        private void ChangeLaserChargeText(int laserCharges)
        {
            OnLaserChargesChanged?.Invoke(laserCharges);
        }

        private void ChangeLaserReloadText(float timer)
        {
            OnLaserTimeChanged?.Invoke(timer);
        }

        private void ShowLosePanel()
        {
            OnPlayerKilled?.Invoke(_gameSessionData.Points, _gameSessionData.PreviousPoints);
        }
    }
}
