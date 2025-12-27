using System; 
using System.Linq; 
using System.Collections.Generic;
using _Project.Scripts.Low;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
    public class PlayerStatsHudPresenter : IInitializable, IDisposable
    {
        protected SceneController _sceneController;
        protected PlayerStatsHudModel _model;
        protected PlayerStatsHudVIew _view;

        private Action onRestart;

        [Inject]
        private void Inject(SceneController sceneController, PlayerStatsHudModel model, PlayerStatsHudVIew view)
        {
            _sceneController = sceneController;
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
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

        private void ShowLosePanel()
        {
            _view.ShowLosePanel();
        }
    }
}