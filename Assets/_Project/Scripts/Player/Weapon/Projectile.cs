using UnityEngine;

namespace AsteroidGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private ObjectPooler _objectPooler;
        
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetPooler(ObjectPooler pooler)
        {
            _objectPooler = pooler;
        }

        private void FixedUpdate()
        {
            _rigidbody.position = transform.position + transform.up * (_speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.Kill();

                if (_objectPooler != null)
                {
                    _objectPooler.ReturnObject(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
