using System; 
using System.Linq; 
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class Presenter : MonoBehaviour
    {
        [Header("Texts With Data")]
        [SerializeField] private TextMeshProUGUI _pointsText;

        [SerializeField] private TextMeshProUGUI _playerCoordinatesText;
        [SerializeField] private TextMeshProUGUI _playerAngleText;
        [SerializeField] private TextMeshProUGUI _laserChargesText;
        [SerializeField] private TextMeshProUGUI _laserReloadTimeText;

        [Header("Final Panel")]
        [SerializeField] private GameObject _losePanel;

        [SerializeField] private Button _restartButton;

        public void InitializeRestartButton(Action clickEvent)
        {
            if(_restartButton == null) return;
            
            _restartButton.onClick.AddListener(() => { clickEvent.Invoke(); });
        }

        public void ChangePointsText(string pointsString)
        {
            if (_pointsText == null) return;

            _pointsText.text = pointsString;
        }

        public void ChangePlayerCoordinatesText(string playerPosString)
        {
            if (_playerCoordinatesText == null) return;

            _playerCoordinatesText.text = playerPosString;
        }

        public void ChangePlayerAngleText(string playerAngleText)
        {
            if (_playerAngleText == null) return;

            _playerAngleText.text = playerAngleText;
        }

        public void ChangeLaserChargeText(string laserChargesString)
        {
            if (_laserChargesText == null) return;

            _laserChargesText.text = laserChargesString;
        }

        public void ChangeLaserReloadText(string timeToReloadLaser)
        {
            if (_laserReloadTimeText == null) return;

            _laserReloadTimeText.text = timeToReloadLaser;
        }

        public void ShowLosePanel()
        {
            if (_losePanel == null) return;
            
            _losePanel.SetActive(true);
        }
    }
}