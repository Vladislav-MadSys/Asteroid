using System;
using _Project.Scripts.Low;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class PlayerStatsHudPresenter : IDisposable
    {
        protected SceneController _sceneController;
        protected PlayerStatsHudModel _model;
        protected PlayerStatsHudView _view;

        private Action onRestart;

        public void Initialize(SceneController sceneController, PlayerStatsHudModel model, PlayerStatsHudView view)
        {
            _sceneController = sceneController;
            _model = model;
            _view = view;
        
            _model.OnPointsChanged += ChangePointsText;
            _model.OnPlayerPositionChanged += ChangePlayerCoordinatesText;
            _model.OnPlayerRotationChanged += ChangePlayerAngleText;
            _model.OnLaserChargesChanged += ChangeLaserChargeText;
            _model.OnLaserTimeChanged += ChangeLaserReloadText;
            _model.OnPlayerKilled += ShowLosePanel;
        }

        public void Dispose()
        {
            _model.OnPointsChanged -= ChangePointsText;
            _model.OnPlayerPositionChanged -= ChangePlayerCoordinatesText;
            _model.OnPlayerRotationChanged -= ChangePlayerAngleText;
            _model.OnLaserChargesChanged -= ChangeLaserChargeText;
            _model.OnLaserTimeChanged -= ChangeLaserReloadText;
            _model.OnPlayerKilled -= ShowLosePanel;
        }
        
        

        public void OnRestartButtonClicked()
        {
            _sceneController.ReloadCurrentScene();
        }
        
        private void ChangePointsText(int points)
        {
            _view.ChangePointsText(points.ToString());
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

        private void ShowLosePanel(int currentPoints, int previousPoints)
        {
            _view.ShowLosePanel(currentPoints, previousPoints);
        }
    }
}