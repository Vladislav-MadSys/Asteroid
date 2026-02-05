using _Project.Scripts.Config;
using _Project.Scripts.GameEntities.Player;
using _Project.Scripts.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GameEntities.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _minRotationOffset = -5;
        [SerializeField] private float _maxRotationOffset = 5;

        private PlayerFactory _playerFactory;
        private Transform _transform;
        private Rigidbody2D _rigidbody;
    
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(ConfigData configData, PlayerFactory playerFactory)
        {
            _speed = configData.AsteroidSpeed;
            _playerFactory = playerFactory;
        }

        public void SetStartDirection()
        {
            Vector3 direction = Vector3.zero - _transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            _transform.Rotate(0, 0, Random.Range(_minRotationOffset, _maxRotationOffset));
        }

        private void FixedUpdate()
        {
            if (_playerFactory.PlayerShip == null || (_playerFactory.PlayerShip != null && _playerFactory.PlayerShip.IsDead == true)) return;
            
            _rigidbody.position = _transform.position + _transform.up * (_speed * Time.fixedDeltaTime);
        }
    }
}
