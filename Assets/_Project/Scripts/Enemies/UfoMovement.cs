using UnityEngine;
using Zenject;

namespace AsteroidGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UfoMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;

        private Transform _transform;
        private Transform _target;
        private Rigidbody2D _rigidbody;

        public void Initialize(PlayerShip player)
        {
            if (player != null)
            {
                _target = player.transform;
            }
        }

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_target != null)
            {
                _rigidbody.position =
                    Vector3.MoveTowards(_transform.position, _target.position, Time.deltaTime * _speed);
            }
        }
    }
}