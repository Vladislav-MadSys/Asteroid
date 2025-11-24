using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Zenject.SpaceFighter;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovmentController : MonoBehaviour
{
    [field: SerializeField] private float _speed = 1;
    [field: SerializeField] private float _rotationSpeed = 15;
    [field: SerializeField] private float _movmentInertia = 1;
    [field: SerializeField] private float _rotationInertia = 1;


    private PlayerInputHandler _playerInputHandler;

    private Rigidbody2D _rb;
    private Transform _transform;

    [Inject]
    void Inject(PlayerInputHandler playerInputHandler)
    {
        _playerInputHandler = playerInputHandler;
    }

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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

        GameEvents.ChangePlayerPosition(new Vector2(_transform.position.x, _transform.position.y));
        GameEvents.ChangePlayerRotation(_transform.rotation.z*180);
    }
    
    private void OnDestroy()
    {
        _playerInputHandler.Dispose();
    }
}
