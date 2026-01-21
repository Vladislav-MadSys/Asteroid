using _Project.Scripts.GameEntities.Enemies;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;
using PlayerInputHandler = _Project.Scripts.Low.PlayerInputHandler;

namespace _Project.Scripts.GameEntities.Player.Weapon
{
    public class Laser : MonoBehaviour
    {
        private const float RANGE = 10;
        private const int LASER_BUFFER_SIZE = 50;

        public bool ControlEnabled = true;
        
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _shootingDelay = 2f;
        [SerializeField] private float _maxCharges = 3;
        [SerializeField] private float _chargeDelay = 5f;

        private Transform _transform;
        private PlayerStates _playerStates;
        private PlayerInputHandler _playerInputHandler;
        private GameSessionData _gameSessionData;

        private float _shootingTimer;
        private RaycastHit2D[] _hitsBuffer = new RaycastHit2D[LASER_BUFFER_SIZE];

        private int _curCharges = 0;
        private float _chargeTimer;
        private bool _canFire = false;
        
        public void Initialize(PlayerInputHandler playerInputHandler, PlayerStates playerStates, GameSessionData gameSessionData)
        {
            _playerInputHandler = playerInputHandler;
            _playerStates = playerStates;
            _gameSessionData = gameSessionData;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (!ControlEnabled) return;
            
            if (_canFire)
            {
                if (_curCharges > 0)
                {
                    if (_playerInputHandler.isFire2ButtonPressed)
                    {
                        Fire();
                        _curCharges -= 1;
                        _playerStates.ChangeLaserCharges(_curCharges);
                        _canFire = false;
                        _shootingTimer = 0;
                    }
                }
            }
            else
            {
                if (_shootingTimer >= _shootingDelay)
                {
                    _canFire = true;
                    if (_lineRenderer)
                    {
                        _lineRenderer.SetPosition(0, _transform.position);
                        _lineRenderer.SetPosition(1, _transform.position);
                    }
                }
                else
                {
                    _shootingTimer += Time.deltaTime;
                }
            }

            if (_curCharges < _maxCharges)
            {
                if (_chargeTimer >= _chargeDelay)
                {
                    _curCharges++;
                    _playerStates.ChangeLaserCharges(_curCharges);
                    _chargeTimer = 0;
                }
                else
                {
                    _chargeTimer += Time.deltaTime;
                }

                _playerStates.ChangeLaserTime(_chargeTimer);
            }
        }

        protected void Fire()
        {
            Vector2 startPoint = transform.position;
            Vector2 endPoint = startPoint + (Vector2)_transform.up * RANGE;

            if (_lineRenderer)
            {
                _lineRenderer.SetPosition(0, startPoint);
                _lineRenderer.SetPosition(1, endPoint);
            }

            int hitsCount = Physics2D.RaycastNonAlloc(startPoint, endPoint, _hitsBuffer);

            for (int i = 0; i < hitsCount; i++)
            {
                RaycastHit2D hit = _hitsBuffer[i];
                if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.Kill();
                }
            }
            _gameSessionData.AddLaserUses();
        }
    }
}
