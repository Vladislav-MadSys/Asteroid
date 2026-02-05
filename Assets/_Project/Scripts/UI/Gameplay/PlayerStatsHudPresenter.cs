using System;
using _Project.Scripts.Advertisement;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Low;
using _Project.Scripts.Low.SceneController;
using _Project.Scripts.Purchases;
using _Project.Scripts.Saves;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class PlayerStatsHudPresenter : IDisposable
    {
        protected ISceneController _sceneController;
        protected PlayerFactory _playerFactory;
        protected IAdvertisement _advertisement;
        protected PlayerStatsHudModel _model;
        protected PlayerStatsHudView _view;
        protected IPurchaser _purchaser;

        private Action onRestart;

        public void Initialize(
            ISceneController sceneController, 
            IAdvertisement advertisment, 
            PlayerFactory playerFactory,
            IPurchaser purchaser,
            PlayerStatsHudModel model,
            PlayerStatsHudView view
            )
        {
            _sceneController = sceneController;
            _advertisement = advertisment;
            _playerFactory = playerFactory;
            _purchaser = purchaser;
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

        public void OnGoToMainMenuButtonClicked()
        {
            _sceneController.LoadMainMenuScene();
        }
        
        public void OnRespawnButtonClicked()
        {
            _advertisement.ShowRewardedAd(() =>
            {
                _playerFactory.PlayerShip.RespawnPlayer();
                _model.wasPlayerRespawned = true;
            });
        }

        public void OnCancelRespawnButtonClicked()
        {
            if (_purchaser.IsAdsRemoved == false)
            {
                _advertisement.ShowInterstitialAd();
            }
        }

        private void ChangePointsText(int points)
        {
            _view.ChangePointsText(points.ToString());
        }

        private void ChangePlayerCoordinatesText(Vector2 newCoordinates)
        {
            _view.ChangePlayerCoordinatesText("Player pos: " + newCoordinates.x.ToString("F2") + " " +  newCoordinates.y.ToString("F2"));
        }

        private void ChangePlayerAngleText(float newAngle)
        {
            _view.ChangePlayerAngleText("Player angle: " + newAngle.ToString("F2"));
        }

        private void ChangeLaserChargeText(int laserCharges)
        {
            _view.ChangeLaserChargeText("Laser charges: " + laserCharges);
        }

        private void ChangeLaserReloadText(float timer)
        {
            _view.ChangeLaserReloadText("Laser reload: " + timer.ToString("F2"));
        }

        private void ShowLosePanel(int currentPoints, int previousPoints)
        {
            _view.ShowLosePanel(currentPoints, previousPoints, !_model.wasPlayerRespawned);
        }
        
        
    }
}