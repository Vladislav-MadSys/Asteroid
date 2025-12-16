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
    public class Model : IInitializable, IDisposable
    {
        private Presenter _presenter;

        private PlayerStates _playerStates;
        private SceneController _sceneController;
        private GameSessionData _gameSessionData;

        [Inject]
        private void Inject(PlayerStates playerStates, SceneController sceneController, GameSessionData gameSessionData, Presenter presenter)
        {
            _playerStates = playerStates;
            _sceneController = sceneController;
            _gameSessionData = gameSessionData;
            _presenter = presenter;
        }

        public void Initialize()
        {
            Debug.Log("Initializing Model");
            _presenter.InitializeRestartButton(() => { _sceneController.ReloadCurrentScene(); });

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
            _presenter.ChangePointsText(_gameSessionData.Points.ToString());
        }

        private void ChangePlayerCoordinatesText(Vector2 newCoordinates)
        {
            _presenter.ChangePlayerCoordinatesText("Player pos: " + newCoordinates);
        }

        private void ChangePlayerAngleText(float newAngle)
        {
            _presenter.ChangePlayerAngleText("Player angle: " + newAngle);
        }

        private void ChangeLaserChargeText(int laserCharges)
        {
            _presenter.ChangeLaserChargeText("Laser charges: " + laserCharges);
        }

        private void ChangeLaserReloadText(float timer)
        {
            _presenter.ChangeLaserReloadText("Laser reload: " + timer);
        }

        private void ShowLosePanel()
        {
            _presenter.ShowLosePanel();
        }
    }
}
