using System;
using System.Threading;
using _Project.Scripts.GameEntities.Enemies;
using _Project.Scripts.Universal;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameEntities.Player.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> OnEndLifeTime;
        
        [SerializeField] private float _speed;
        [SerializeField] private int _lifeTimeMs = 10000;
        
        private Transform _transform;
        private Rigidbody2D _rigidbody;

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
                EndLifetime();
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

        private void FixedUpdate()
        {
            _rigidbody.position = transform.position + transform.up * (_speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.Kill();
                EndLifetime();
            }
        }
        private void EndLifetime()
        {
            _cancellationTokenSource.Cancel();
            OnEndLifeTime?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
