using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _transform;

    [SerializeField] private float _speed;
    
    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.Translate(_transform.up * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Kill();

            Destroy(gameObject);
            //In general, we can use SetActive instead of Destroing for optimization perfomance in moment, 
            //but I think it can lead to troubles with GC
        }
    }
}
