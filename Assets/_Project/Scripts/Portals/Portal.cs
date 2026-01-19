using System;
using _Project.Scripts.GameEntities.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Portals
{
    public class Portal : ILateTickable, IInitializable, IDisposable
    {
        private const float VIEWPORT_SCALE = 1;
        
        private Camera _mainCamera;
        private PlayerFactory _playerFactory;
        private Transform _playerTransform;
        private float _offsetOutOfScreen = 0.05f;

        private Vector2 _cameraOffset;

        [Inject]
        private void Inject(PlayerFactory playerFactory, Camera camera)
        {
            _playerFactory = playerFactory;
            _mainCamera = camera;
            
            Initialize();
        }

        public void Initialize()
        {
            _cameraOffset = new Vector2(_mainCamera.transform.position.x, _mainCamera.transform.position.y);
            _playerFactory.OnPlayerShipCreated += SetPlayer;
        }

        public void Dispose()
        {
            _playerFactory.OnPlayerShipCreated -= SetPlayer;
        }

        public void LateTick()
        {
            if (!_playerTransform) return;
            

            Vector3 viewportPos = _mainCamera.WorldToViewportPoint(_playerTransform.position);
            Vector3 newPosition = _playerTransform.position;
            Vector3 cachedPos = newPosition;

            if (viewportPos.x > VIEWPORT_SCALE + _offsetOutOfScreen || viewportPos.x < -_offsetOutOfScreen ||
                viewportPos.y > VIEWPORT_SCALE + _offsetOutOfScreen || viewportPos.y < -_offsetOutOfScreen)
            {
                if (viewportPos.x > VIEWPORT_SCALE + _offsetOutOfScreen)
                    newPosition.x = -(newPosition.x - _cameraOffset.x) + _cameraOffset.x + _offsetOutOfScreen;
                else if (viewportPos.x < -_offsetOutOfScreen)
                    newPosition.x = -(newPosition.x - _cameraOffset.x) + _cameraOffset.x - _offsetOutOfScreen;

                if (viewportPos.y > VIEWPORT_SCALE + _offsetOutOfScreen)
                    newPosition.y = -(newPosition.y - _cameraOffset.y) + _cameraOffset.y + _offsetOutOfScreen;
                else if (viewportPos.y < -_offsetOutOfScreen)
                    newPosition.y = -(newPosition.y - _cameraOffset.y) + _cameraOffset.y - _offsetOutOfScreen;

                if (cachedPos != newPosition)
                {
                    _playerTransform.position = newPosition;
                }
            }
        }

        private void SetPlayer(PlayerShip playerShip)
        {
            _playerTransform = playerShip.transform;
        }
    }
}
