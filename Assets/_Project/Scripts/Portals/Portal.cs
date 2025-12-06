using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    public class Portal : MonoBehaviour
    {
        private Camera _mainCamera;
        private Transform _playerTransform;
        private float _offsetOutOfScreen = 0.05f;

        private Vector2 _cameraOffset;

        [Inject]
        private void Inject(PlayerShip playerShip, Camera camera)
        {
            _playerTransform = playerShip.transform;
            _mainCamera = camera;
        }

        public void Awake()
        {
            _cameraOffset = new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y);
        }

        private void LateUpdate()
        {
            if (!_playerTransform) return;

            Vector3 viewportPos = _mainCamera.WorldToViewportPoint(_playerTransform.position);
            Vector3 newPosition = _playerTransform.position;
            Vector3 cachedPos = newPosition;

            if (viewportPos.x > 1 + _offsetOutOfScreen || viewportPos.x < -_offsetOutOfScreen ||
                viewportPos.y > 1 + _offsetOutOfScreen || viewportPos.y < -_offsetOutOfScreen)
            {
                if (viewportPos.x > 1 + _offsetOutOfScreen)
                    newPosition.x = -(newPosition.x - _cameraOffset.x) + _cameraOffset.x + _offsetOutOfScreen;
                else if (viewportPos.x < -_offsetOutOfScreen)
                    newPosition.x = -(newPosition.x - _cameraOffset.x) + _cameraOffset.x - _offsetOutOfScreen;

                if (viewportPos.y > 1 + _offsetOutOfScreen)
                    newPosition.y = -(newPosition.y - _cameraOffset.y) + _cameraOffset.y + _offsetOutOfScreen;
                else if (viewportPos.y < -_offsetOutOfScreen)
                    newPosition.y = -(newPosition.y - _cameraOffset.y) + _cameraOffset.y - _offsetOutOfScreen;

                if (cachedPos != newPosition)
                {
                    _playerTransform.position = newPosition;
                }
            }
        }
    }
}
