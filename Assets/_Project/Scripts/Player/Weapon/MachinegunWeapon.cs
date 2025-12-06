using UnityEngine;
using Zenject;

public class MachinegunWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shootingDelay = 0.5f;
    
    private Transform _transform;
    private PlayerInputHandler _playerInputHandler;

    private float _timer;
    private bool _canFire = false;

    [Inject]
    private void Inject(PlayerInputHandler playerInputHandler)
    {
        _playerInputHandler = playerInputHandler;
    }
    
    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if(_canFire)
        {
            if(_playerInputHandler.isFireButtonPressed)
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
        GameObject projectile = Instantiate(_projectilePrefab, _transform);
        projectile.transform.rotation = _transform.rotation;
        projectile.transform.parent = null;
    }
}
