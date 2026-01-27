using _Project.Scripts.Config;
using _Project.Scripts.GameEntities.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UfoMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;

        private Transform _transform;
        private PlayerFactory _playerFactory;
        private PlayerShip _playerShip;
        private Transform _target;
        private Rigidbody2D _rigidbody;

        public void Initialize(PlayerFactory playerFactory, ConfigData configData)
        {
            _playerFactory = playerFactory;
            if(playerFactory.PlayerShip != null)
            {
                _playerShip = playerFactory.PlayerShip;
                _target = playerFactory.PlayerShip.transform;
            }
            else
            {
                playerFactory.OnPlayerShipCreated += UpdatePlayer;
            }
            _speed = configData.UfoSpeed;
        }

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
            if (_playerShip != null)
            {
                _playerFactory.OnPlayerShipCreated -= UpdatePlayer;
            }
        }
        
        private void UpdatePlayer(PlayerShip playerShip)
        {
            _playerShip = playerShip;
            _target = playerShip.transform;
        }

        private void FixedUpdate()
        {
            if (_target != null)
            {
                if (!_playerShip.IsDead)
                {
                    _rigidbody.position =
                        Vector3.MoveTowards(_transform.position, _target.position, Time.deltaTime * _speed);
                }
            }
        }
    }
}