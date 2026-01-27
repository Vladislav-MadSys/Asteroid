using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Purchases
{
    public class PurchaserInitializator : MonoBehaviour
    {
        [SerializeField] private IAPListener _iapListener;
        private IPurchaser _purchaser;

        [Inject]
        private void Inject(IPurchaser purchaser)
        {
            _purchaser = purchaser;
        }
    
        private void Awake()
        {
            _iapListener.onPurchasesFetched.AddListener(_purchaser.OnPurchasesFetched);
            _iapListener.onOrderPending.AddListener(_purchaser.OnOrderPending);
        }

        private void OnDestroy()
        {
            _iapListener.onPurchasesFetched.RemoveListener(_purchaser.OnPurchasesFetched);
            _iapListener.onOrderPending.RemoveListener(_purchaser.OnOrderPending);
        }
    }
}
