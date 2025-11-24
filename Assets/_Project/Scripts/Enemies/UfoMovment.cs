using UnityEngine;
using Zenject;

public class UfoMovment : MonoBehaviour
{
    private Transform _transform;
    private Transform _target;

    [SerializeField] private float _speed = 5;

    private void Awake()
    {
        _transform = transform;

        //Know that it's very bad practice, but here is no any reason to find a way to make it 
        //fast and clear throw zenject
        //PlayerShip player = FindFirstObjectByType<PlayerShip>();
        
    }

    public void Initialize(PlayerShip player)
    {
        if (player)
        {
            _target = player.transform;
        }
    }
    

    private void Update()
    {
        if (_target)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _target.position, Time.deltaTime * _speed);
        }
    }
}
