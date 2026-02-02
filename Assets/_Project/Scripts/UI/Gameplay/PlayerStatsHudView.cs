using _Project.Scripts.Saves;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay
{
    public class PlayerStatsHudView : MonoBehaviour
    {
    
        protected PlayerStatsHudPresenter _presenter;
    
        [Header("Texts With Data")]
        [SerializeField] private TextMeshProUGUI _pointsText;
        [SerializeField] private TextMeshProUGUI _playerCoordinatesText;
        [SerializeField] private TextMeshProUGUI _playerAngleText;
        [SerializeField] private TextMeshProUGUI _laserChargesText;
        [SerializeField] private TextMeshProUGUI _laserReloadTimeText;
        
        [Header("Final Panel")]
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private TextMeshProUGUI _losePanelPointsText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _goToMainMenuButton;
        [SerializeField] private GameObject _respawnPanel;
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Button _cancelRespawnButton;

        private UnityAction OnRestartButtonClickEvent;
        private UnityAction OnRespawnButtonClickEvent;
        private UnityAction OnGoToMainMenuButtonClickEvent;
        private UnityAction OnCancelRespawnButtonClickEvent;
        private UnityAction OnSelectLocalSaveButtonClickEvent;
        private UnityAction OnSelectCloudSaveButtonClickEvent;

        public void Initialize(PlayerStatsHudPresenter presenter)
        {
            _presenter = presenter;
        }
    
    
        public void Awake()
        {
            _losePanel.SetActive(false);
                
            OnRestartButtonClickEvent = () =>
            {
                _presenter.OnRestartButtonClicked();
            };
            _restartButton.onClick.AddListener(OnRestartButtonClickEvent);

            OnRespawnButtonClickEvent = () =>
            {
                _losePanel.SetActive(false);
                _presenter.OnRespawnButtonClicked();
            };
            _respawnButton.onClick.AddListener(OnRespawnButtonClickEvent);
            
            OnCancelRespawnButtonClickEvent = () =>
            {
                _respawnPanel.SetActive(false);
                _presenter.OnCancelRespawnButtonClicked();
            };
            _cancelRespawnButton.onClick.AddListener(OnCancelRespawnButtonClickEvent);

            OnGoToMainMenuButtonClickEvent = () =>
            {
                _presenter.OnGoToMainMenuButtonClicked();
            };
            _goToMainMenuButton.onClick.AddListener(OnGoToMainMenuButtonClickEvent);
        }
        
        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClickEvent);
            _respawnButton.onClick.RemoveListener(OnRespawnButtonClickEvent);
        }

        public void ChangePointsText(string pointsString)
        {
            _pointsText.text = pointsString;
        }

        public void ChangePlayerCoordinatesText(string playerPosString)
        {
            _playerCoordinatesText.text = playerPosString;
        }

        public void ChangePlayerAngleText(string playerAngleText)
        {
            _playerAngleText.text = playerAngleText;
        }

        public void ChangeLaserChargeText(string laserChargesString)
        {
            _laserChargesText.text = laserChargesString;
        }

        public void ChangeLaserReloadText(string timeToReloadLaser)
        {
            _laserReloadTimeText.text = timeToReloadLaser;
        }

        public void ShowLosePanel(int currentPoints, int previousPoints, bool canRespawnPlayer)
        {
            _losePanel.SetActive(true);
            _respawnButton.interactable = canRespawnPlayer;
            _respawnPanel.gameObject.SetActive(canRespawnPlayer);
            _losePanelPointsText.text = "Current points: " + currentPoints + '\n' + "Previous Points: " + previousPoints;
        }
        
        
    }
}
