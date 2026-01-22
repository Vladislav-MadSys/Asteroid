using _Project.Scripts.Addressables;
using _Project.Scripts.Low;
using _Project.Scripts.Services;
using _Project.Scripts.Universal;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEntities.Player.Weapon
{
    public class MachinegunWeapon : MonoBehaviour
    {
        public bool ControlEnabled = true;
        
        [SerializeField]private string _projectilePrefabKey;
        [SerializeField] private float _shootingDelay = 0.5f;
        
        private GameObject _projectilePrefab;
        private Transform _transform;
        private ObjectPool<Projectile> _objectPool;
        private PlayerInputHandler _playerInputHandler;
        private GameSessionData _gameSessionData;
        private IResourcesService _resourceService;
        
        
        private float _timer;
        private bool _canFire = false;
        private bool _isReady = false;

        public async void Initialize(PlayerInputHandler playerInputHandler, GameSessionData gameSessionData, IResourcesService resourceService, ConfigData configData)
        {
            _playerInputHandler = playerInputHandler;
            _gameSessionData = gameSessionData;
            _resourceService = resourceService;
            
            _transform = transform;
            _shootingDelay = configData.ShootDelay;
            _projectilePrefab = await _resourceService.Load<GameObject>(AddressablesKeys.PROJECTILE);
            _objectPool = new ObjectPool<Projectile>(_projectilePrefab, 50);
            _objectPool.Initialize();
            _isReady = true;
        }

        private void Update()
        {
            if(!_isReady || !ControlEnabled) return;
                
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
            Projectile projectile = _objectPool.GetObject();
            projectile.gameObject.SetActive(true);
            Transform projectileTransform = projectile.transform;
            projectileTransform.position = _transform.position;
            projectileTransform.rotation = _transform.rotation;
            projectileTransform.parent = null;
            _gameSessionData.AddShot();
        }

        private void OnProjectileEndLifeTime(Projectile projectile)
        {
            _objectPool.ReturnObject(projectile);
            projectile.OnEndLifeTime -= OnProjectileEndLifeTime;
        }
        
    }
}
