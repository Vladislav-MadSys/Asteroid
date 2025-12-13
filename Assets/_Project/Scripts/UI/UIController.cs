using _Project.Scripts.Low;
using _Project.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [Header("Texts With Data")] [SerializeField]
        private TextMeshProUGUI _pointsText;

        [SerializeField] private TextMeshProUGUI _playerCoordinatesText;
        [SerializeField] private TextMeshProUGUI _playerAngleText;
        [SerializeField] private TextMeshProUGUI _laserChargesText;
        [SerializeField] private TextMeshProUGUI _laserReloadTimeText;

        [Header("Final Panel")] [SerializeField]
        private GameObject _losePanel;

        [SerializeField] private Button _restartButton;

        private PlayerStates _playerStates;
        private SceneController _sceneController;
        private GameSessionData _gameSessionData;

        [Inject]
        private void Inject(PlayerStates playerStates, SceneController sceneController, GameSessionData gameSessionData)
        {
            _playerStates = playerStates;
            _sceneController = sceneController;
            _gameSessionData = gameSessionData;
        }

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => { _sceneController.ReloadCurrentScene(); });

            _gameSessionData.OnPointsChanged += ChangePointsText;
            _playerStates.OnPlayerPositionChanged += ChangePlayerCoordinatesText;
            _playerStates.OnPlayerRotationChanged += ChangePlayerAngleText;
            _playerStates.OnLaserChargesChanged += ChangeLaserChargeText;
            _playerStates.OnLaserTimeChangedChanged += ChangeLaserReloadText;

            _gameSessionData.OnPlayerKilled += ShowLosePanel;
        }

        private void OnDestroy()
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
            if (_pointsText == null) return;

            _pointsText.text = _gameSessionData.Points.ToString();
        }

        private void ChangePlayerCoordinatesText(Vector2 newCoordinates)
        {
            if (_playerCoordinatesText == null) return;

            _playerCoordinatesText.text = "Player pos: " + newCoordinates;
        }

        private void ChangePlayerAngleText(float newAngle)
        {
            if (_playerAngleText == null) return;

            _playerAngleText.text = "Player angle: " + newAngle;
        }

        private void ChangeLaserChargeText(int laserCharges)
        {
            if (_laserChargesText == null) return;

            _laserChargesText.text = "Laser charges: " + laserCharges;
        }

        private void ChangeLaserReloadText(float timer)
        {
            if (_laserReloadTimeText == null) return;

            _laserReloadTimeText.text = "Laser reload: " + timer;
        }

        private void ShowLosePanel()
        {
            _losePanel.SetActive(true);
        }
    }
}