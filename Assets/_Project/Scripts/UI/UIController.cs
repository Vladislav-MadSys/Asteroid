using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private TextMeshProUGUI _playerCoordinatesText;
    [SerializeField] private TextMeshProUGUI _playerAngleText;
    [SerializeField] private TextMeshProUGUI _laserChargesText;
    [SerializeField] private TextMeshProUGUI _laserReloadTimeText;

    private void OnEnable()
    {
        GameEvents.OnPointsChanged += ChangePointsText;
        GameEvents.OnPlayerPositionChanged += ChangePlayerCoordinatesText;
        GameEvents.OnPlayerRotationChanged += ChangePlayerAngleText;
        GameEvents.OnLaserChargesChanged += ChangeLaserChargeText;
        GameEvents.OnLaserTimeChangedChanged += ChangeLaserReloadText;
    }

    private void OnDisable()
    {
        GameEvents.OnPointsChanged -= ChangePointsText;
        GameEvents.OnPlayerPositionChanged -= ChangePlayerCoordinatesText;
        GameEvents.OnPlayerRotationChanged -= ChangePlayerAngleText;
        GameEvents.OnLaserChargesChanged -= ChangeLaserChargeText;
        GameEvents.OnLaserTimeChangedChanged -= ChangeLaserReloadText;
    }

    void ChangePointsText(int points)
    {
        if (_pointsText == null) return;

        _pointsText.text = points.ToString();
    }

    void ChangePlayerCoordinatesText(Vector2 newCoordinates)
    {
        if (_playerCoordinatesText == null) return;

        _playerCoordinatesText.text = "Player pos: " + newCoordinates;
    }

    void ChangePlayerAngleText(float newAngle)
    {
        if (_playerAngleText == null) return;

        _playerAngleText.text = "Player angle: " + newAngle;
    }

    void ChangeLaserChargeText(int laserCharges)
    {
        if (_laserChargesText == null) return;

        _laserChargesText.text = "Laser charges: " + laserCharges;
    }

    void ChangeLaserReloadText(float timer)
    {
        if (_laserReloadTimeText == null) return;

        _laserReloadTimeText.text = "Laser reload: " + timer;
    }
}
