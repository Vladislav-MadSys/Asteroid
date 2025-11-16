using UnityEngine;
using Zenject.Asteroids;

public class AsteroidMovment : MonoBehaviour
{
    private Transform _transform;

    [SerializeField] private float _speed;
    [SerializeField] private float minRotationOffset = -5;
    [SerializeField] private float maxRotationOffset = 5;
    [SerializeField] private float lifetime = 30;

    private void Awake()
    {
        _transform = transform;
        Vector3 direction = Vector3.zero - _transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        _transform.Rotate(0, 0, Random.Range(minRotationOffset, maxRotationOffset));
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        _transform.Translate(_transform.up * _speed * Time.deltaTime, Space.World);
    }

    private void OnDestroy()
    {
        
    }
}
