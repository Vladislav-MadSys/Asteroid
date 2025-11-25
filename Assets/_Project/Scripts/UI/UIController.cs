using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

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

    private GameEvents _gameEvents;
    private SceneController _sceneController;

    [Inject]
    void Inject(GameEvents gameEvents, SceneController sceneController)
    {
        _gameEvents = gameEvents;
        _sceneController = sceneController;
    }
    
    private void Awake()
    {
        _restartButton.onClick.AddListener(() => { 
             _sceneController.ReloadCurrentScene();
        });
        
        _gameEvents.OnPointsChanged += ChangePointsText;
        _gameEvents.OnPlayerPositionChanged += ChangePlayerCoordinatesText;
        _gameEvents.OnPlayerRotationChanged += ChangePlayerAngleText;
        _gameEvents.OnLaserChargesChanged += ChangeLaserChargeText;
        _gameEvents.OnLaserTimeChangedChanged += ChangeLaserReloadText;

        _gameEvents.OnPlayerKilled += ShowLosePanel;
    }

    private void OnDestroy()
    {
        _gameEvents.OnPointsChanged -= ChangePointsText;
        _gameEvents.OnPlayerPositionChanged -= ChangePlayerCoordinatesText;
        _gameEvents.OnPlayerRotationChanged -= ChangePlayerAngleText;
        _gameEvents.OnLaserChargesChanged -= ChangeLaserChargeText;
        _gameEvents.OnLaserTimeChangedChanged -= ChangeLaserReloadText;

        _gameEvents.OnPlayerKilled -= ShowLosePanel;
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
