using System;
using UnityEngine;

namespace AsteroidGame
{
    public class Enemy : MonoBehaviour
    {
        public event Action<int> OnKill;

        [dield: SerializeField] public int PointsForKill {get; private set; }

        protected ObjectPooler _objectPooler; 

        public void Initialize(ObjectPooler objectPooler)
        {
            _objectPooler = objectPooler;
        }

        public virtual void Kill()
        {
            OnKill?.Invoke(PointsForKill);

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