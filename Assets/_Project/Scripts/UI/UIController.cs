using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
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

    private void Awake()
    {
        _restartButton.onClick.AddListener(() => { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Little hack
        });
        
        GameEvents.OnPointsChanged += ChangePointsText;
        GameEvents.OnPlayerPositionChanged += ChangePlayerCoordinatesText;
        GameEvents.OnPlayerRotationChanged += ChangePlayerAngleText;
        GameEvents.OnLaserChargesChanged += ChangeLaserChargeText;
        GameEvents.OnLaserTimeChangedChanged += ChangeLaserReloadText;

        GameEvents.OnPlayerKilled += ShowLosePanel;
    }

    private void OnDestroy()
    {
        GameEvents.OnPointsChanged -= ChangePointsText;
        GameEvents.OnPlayerPositionChanged -= ChangePlayerCoordinatesText;
        GameEvents.OnPlayerRotationChanged -= ChangePlayerAngleText;
        GameEvents.OnLaserChargesChanged -= ChangeLaserChargeText;
        GameEvents.OnLaserTimeChangedChanged -= ChangeLaserReloadText;

        GameEvents.OnPlayerKilled -= ShowLosePanel;
    }

    void ChangePointsText(int points)
    {
        if (!_pointsText ) return;

        _pointsText.text = points.ToString();
    }

    void ChangePlayerCoordinatesText(Vector2 newCoordinates)
    {
        if (!_playerCoordinatesText) return;

        _playerCoordinatesText.text = "Player pos: " + newCoordinates;
    }

    void ChangePlayerAngleText(float newAngle)
    {
        if (!_playerAngleText) return;

        _playerAngleText.text = "Player angle: " + newAngle;
    }

    void ChangeLaserChargeText(int laserCharges)
    {
        if (!_laserChargesText) return;

        _laserChargesText.text = "Laser charges: " + laserCharges;
    }

    void ChangeLaserReloadText(float timer)
    {
        if (!_laserReloadTimeText) return;

        _laserReloadTimeText.text = "Laser reload: " + timer;
    }

    void ShowLosePanel()
    {
        _losePanel.SetActive(true);
    }
}
