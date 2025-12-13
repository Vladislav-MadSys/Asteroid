using UnityEngine;

namespace _Project.Scripts.Universal
{
    public class DestroyOnTimer : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy = 30;

        private void Awake()
        {
            Destroy(gameObject, _timeToDestroy);
        }
    }
}
