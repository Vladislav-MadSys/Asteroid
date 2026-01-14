using _Project.Scripts.Low;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Player.Weapon
{
    public class MachinegunWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _shootingDelay = 0.5f;

        private Transform _transform;
        private ObjectPooler _objectPooler;
        private PlayerInputHandler _playerInputHandler;
        private GameSessionData _gameSessionData;
        
        
        private float _timer;
        private bool _canFire = false;

        [Inject]
        private void Inject(PlayerInputHandler playerInputHandler, GameSessionData gameSessionData)
        {
            _playerInputHandler = playerInputHandler;
            _gameSessionData = gameSessionData;
        }

        private void Awake()
        {
            _transform = transform;
            _objectPooler = new ObjectPooler(_projectilePrefab, 50);
            _objectPooler.Initialize();
        }

        private void Update()
        {
            if (_canFire)
            {
                if (_playerInputHandler.isFireButtonPressed)
                {
                    Fire();
                    _canFire = false;
                    _timer = 0;
                }
            }
            else
            {
                if (_timer >= _shootingDelay)
                {
                    _canFire = true;
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }
        }

        private void Fire()
        {
            GameObject projectile = _objectPooler.GetObject();
            Transform projectileTransform = projectile.transform;
            projectileTransform.position = _transform.position;
            projectileTransform.rotation = _transform.rotation;
            projectileTransform.parent = null;
            if (projectile.TryGetComponent(out Projectile projectileScript))
            {
                projectileScript.OnEndLifeTime += OnProjectileEndLifeTime;
            }
            _gameSessionData.AddShot();
        }

        private void OnProjectileEndLifeTime(Projectile projectile)
        {
            _objectPooler.ReturnObject(projectile.gameObject);
            projectile.OnEndLifeTime -= OnProjectileEndLifeTime;
        }
        
    }
}
