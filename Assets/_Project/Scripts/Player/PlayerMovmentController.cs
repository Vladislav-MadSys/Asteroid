using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Zenject.SpaceFighter;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovmentController : MonoBehaviour
{
    [field: SerializeField] private float speed = 1;
    [field: SerializeField] private float rotationSpeed = 15;
    [field: SerializeField] private float movmentInertia = 1;
    [field: SerializeField] private float rotationInertia = 1;


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
            _transform.up * speed * forceInput,
            speed / movmentInertia * Time.deltaTime);

        _rb.angularVelocity = Mathf.Lerp(
            _rb.angularVelocity,
            angularDirection * rotationSpeed,
            rotationSpeed / rotationInertia * Time.deltaTime);

        GameEvents.ChangePlayerPosition(new Vector2(_transform.position.x, _transform.position.y));
        GameEvents.ChangePlayerRotation(_transform.rotation.z*180);
    }

    //Hack
    private void OnDestroy()
    {
        _playerInputHandler.Dispose();
    }
}
