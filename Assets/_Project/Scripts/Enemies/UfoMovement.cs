using UnityEngine;
using Zenject;

public class UfoMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    
    private Transform _transform;
    private Transform _target;

    public void Initialize(PlayerShip player)
    {
        if (player)
        {
            _target = player.transform;
        }
    }
    
    private void Awake()
    {
        _transform = transform;
    }
    
    private void Update()
    {
        if (_target)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _target.position, Time.deltaTime * _speed);
        }
    }
}
