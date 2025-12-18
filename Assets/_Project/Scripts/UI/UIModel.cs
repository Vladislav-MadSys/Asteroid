using System;
using _Project.Scripts.Low;
using _Project.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _Project.Scripts.UI;

namespace _Project.Scripts.UI
{
    public class UIModel : IInitializable, IDisposable
    {
        protected UIVIew _view;

        private PlayerStates _playerStates;
        private SceneController _sceneController;
        private GameSessionData _gameSessionData;

        public int Points => _gameSessionData.Points;

        [Inject]
        private void Inject(PlayerStates playerStates, SceneController sceneController, GameSessionData gameSessionData, UIVIew view)
        {
            _playerStates = playerStates;
            _sceneController = sceneController;
            _gameSessionData = gameSessionData;
            _view = view;
        }

        public void Initialize()
        {
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
            _view.ChangePointsText(Points.ToString());
        }

        private void ChangePlayerCoordinatesText(Vector2 newCoordinates)
        {
            _view.ChangePlayerCoordinatesText("Player pos: " + newCoordinates);
        }

        private void ChangePlayerAngleText(float newAngle)
        {
            _view.ChangePlayerAngleText("Player angle: " + newAngle);
        }

        private void ChangeLaserChargeText(int laserCharges)
        {
            _view.ChangeLaserChargeText("Laser charges: " + laserCharges);
        }

        private void ChangeLaserReloadText(float timer)
        {
            _view.ChangeLaserReloadText("Laser reload: " + timer);
        }

        private void ShowLosePanel()
        {
            _view.ShowLosePanel();
        }
    }
}
