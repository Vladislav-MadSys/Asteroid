 using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class Laser : MonoBehaviour
{
    private const float RANGE = 10;
    
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _shootingDelay = 2f;
    [SerializeField] private float _maxCharges = 3;
    [SerializeField] private float _chargeDelay = 5f;
    
    private Transform _transform;
    private PlayerInputHandler _playerInputHandler;
    
    //Fire
    private float _shootingTimer;

    //Charges
    private int _curCharges = 0;
    private float _chargeTimer;
    private bool _canFire = false;
    
    [Inject]
    void Inject(PlayerInputHandler playerInputHandler)
    {
        _playerInputHandler = playerInputHandler;
    }
    private void Awake()
    {
        _transform = transform;
    }
    private void Update()
    {
        //Shooting
        if (_canFire)
        {
            if (_curCharges > 0)
            {
                if (_playerInputHandler.isFire2ButtonPressed)
                {
                    Fire();
                    _curCharges -= 1;
                    GameEvents.ChangeLaserCharges(_curCharges);
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
                if (!_lineRenderer)
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

        //Charges
        if(_curCharges < _maxCharges)
        {
            if(_chargeTimer >= _chargeDelay)
            {
                _curCharges++;
                GameEvents.ChangeLaserCharges(_curCharges);
                _chargeTimer = 0;
            }
            else
            {
                _chargeTimer += Time.deltaTime;
            }
            GameEvents.ChangeLaserTime(_chargeTimer);
        }

    }

    void Fire()
    {
        Vector2 startPoint = transform.position;
        Vector2 endPoint = startPoint + (Vector2)_transform.up * RANGE;

        if (!_lineRenderer)
        {
            _lineRenderer.SetPosition(0, startPoint);
            _lineRenderer.SetPosition(1, endPoint);
        }

        RaycastHit2D[] hits = Physics2D.LinecastAll(startPoint, endPoint);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Kill();
            }
        }
    }
}
