using _Project.Scripts.Config;
using _Project.Scripts.Low;
using _Project.Scripts.Saves;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        public bool ControlEnabled = true;
        
        [field: SerializeField] private float _speed = 1;
        [field: SerializeField] private float _rotationSpeed = 15;
        [field: SerializeField] private float _movmentInertia = 1;
        [field: SerializeField] private float _rotationInertia = 1;

        private PlayerInputHandler _playerInputHandler;
        private PlayerStates _playerStates;

        private Rigidbody2D _rb;
        private Transform _transform;

        public void Initialize(PlayerInputHandler playerInputHandler, PlayerStates playerStates, SceneSaveController sceneSaveController, ConfigData configData)
        {
            _playerInputHandler = playerInputHandler;
            _playerStates = playerStates;

            _speed = configData.ShipSpeed;
        }

        private void Awake()
        {
            _transform = transform;
            _rb = GetComponent<Rigidbody2D>();
            
        }

        private void OnDestroy()
        { 
            _playerInputHandler.Dispose();
        }

        private void Update()
        {
            if(!ControlEnabled) return;
            
            float angularDirection = -_playerInputHandler.JoyInput.x;
            float forceInput = Mathf.Clamp01(_playerInputHandler.JoyInput.y);

            _rb.linearVelocity = Vector2.Lerp(
                _rb.linearVelocity,
                _transform.up * (_speed * forceInput),
                _speed / _movmentInertia * Time.deltaTime);

            _rb.angularVelocity = Mathf.Lerp(
                _rb.angularVelocity,
                angularDirection * _rotationSpeed,
                _rotationSpeed / _rotationInertia * Time.deltaTime);

            _playerStates.ChangePlayerPosition(new Vector2(_transform.position.x, _transform.position.y));
            _playerStates.ChangePlayerRotation(_transform.rotation.z * 180);
        }

        
        
        public void SetPlayerPosition(Vector2 position)
        {
            _transform.position = position;
        }
        public void SetPlayerRotation(float rotation)
        {
            _transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        }
    }
}
