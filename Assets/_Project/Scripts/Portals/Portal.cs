using UnityEngine;
using Zenject;

public class Portal : MonoBehaviour
{
    private Camera _mainCamera;
    private Transform _playerTransform;

    private Vector2 _cameraOffset;

    [Inject]
    void Inject(PlayerShip playerShip)
    {
        _playerTransform = playerShip.transform;
    }

    public void Awake()
    {
        _mainCamera = Camera.main;
        _cameraOffset = new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y);
    }

    void LateUpdate()
    {
        if (_playerTransform == null) return;

        Vector3 viewportPos = _mainCamera.WorldToViewportPoint(_playerTransform.position);
        Vector3 newPosition = _playerTransform.position;
        Vector3 cachedPos = newPosition;

        if (viewportPos.x > 1.05f || viewportPos.x < -0.05f ||
            viewportPos.y > 1.05f || viewportPos.y < -0.05f)
        {
            if (viewportPos.x > 1.05f)
                newPosition.x = -(newPosition.x - _cameraOffset.x) + _cameraOffset.x + 0.05f;
            else if (viewportPos.x < -0.05f)
                newPosition.x = -(newPosition.x - _cameraOffset.x) + _cameraOffset.x - 0.05f;

            if (viewportPos.y > 1.05f)
                newPosition.y = -(newPosition.y - _cameraOffset.y) + _cameraOffset.y + 0.05f;
            else if (viewportPos.y < -0.05f)
                newPosition.y = -(newPosition.y - _cameraOffset.y) + _cameraOffset.y - 0.05f;

            if (cachedPos != newPosition)
            {
                _playerTransform.position = newPosition;
            }
        }
    }
}
