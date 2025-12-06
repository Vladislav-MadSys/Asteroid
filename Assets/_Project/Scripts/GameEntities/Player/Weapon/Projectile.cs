using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace AsteroidGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _lifeTimeMs = 10000;
        
        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private ObjectPooler _objectPooler;

        private CancellationTokenSource _cancellationTokenSource;
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private async void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await UniTask.Delay(_lifeTimeMs, cancellationToken: _cancellationTokenSource.Token);
                OnEndLifetime();
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
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
                OnEndLifetime();
            }
        }
        private void OnEndLifetime()
        {
            _cancellationTokenSource.Cancel();
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
