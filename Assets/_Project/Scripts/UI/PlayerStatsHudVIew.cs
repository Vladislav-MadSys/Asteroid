using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
    public class PlayerStatsHudVIew : MonoBehaviour
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
        [SerializeField] private Button _restartButton;

        private UnityAction OnRestartButtonClickEvent;

        [Inject]
        private void Inject(PlayerStatsHudPresenter presenter)
        {
            _presenter = presenter;
        }
    
    
        public void Awake()
        {
            OnRestartButtonClickEvent = () =>
            {
                _presenter.OnRestartButtonClicked();
            };
            _restartButton.onClick.AddListener(OnRestartButtonClickEvent);
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

        public void ShowLosePanel()
        {
            _losePanel.SetActive(true);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClickEvent);
        }
    }
}
