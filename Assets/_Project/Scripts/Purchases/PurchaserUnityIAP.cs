using _Project.Scripts.Saves;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Purchases
{
    public class PurchaserUnityIAP : IPurchaser, IInitializable
    {
        private const string REMOVE_ADS_PURCHASE_KEY = "com.DefaultCompany.Asteroid.removeAds";
        private const string BUY_100_RANDOM_SHIT_KEY = "com.DefaultCompany.Asteroid.100RandomConsumableShit";

        public bool IsAdsRemoved { get; protected set; } = false;
    
        private ISaveService _saveService;
    
        [Inject]
        private void Inject(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void Initialize()
        {
            SaveData savedData = _saveService.Load();
            if (savedData != null)
            {
                IsAdsRemoved = savedData.isAdsRemoved;
            }
        }
    
        public void OnOrderPending(PendingOrder order)
        {
            foreach (var cartItem in order.CartOrdered.Items())
            {
                var product = cartItem.Product;
                switch (product.definition.id)
                {
                    case REMOVE_ADS_PURCHASE_KEY:
                        RemoveAds();
                        break;
                    case BUY_100_RANDOM_SHIT_KEY:
                        RandomShit();
                        break;
                }
            }
        }

        public void OnPurchasesFetched(Orders existingOrders)
        {
            foreach (var order in existingOrders.ConfirmedOrders)
            {
                foreach (var cartItem in order.CartOrdered.Items())
                {
                    var product = cartItem.Product;
                    switch (product.definition.id)
                    {
                        case REMOVE_ADS_PURCHASE_KEY:
                            RemoveAds();
                            break;
                        case BUY_100_RANDOM_SHIT_KEY:
                            RandomShit();
                            break;
                    }
                }
            }
        }

        private void RemoveAds()
        {
            IsAdsRemoved = true;
            Debug.Log("Removing Ads");
        }

        private void RandomShit()
        {
            Debug.Log("WOW! You bought 100 random shit!");
        }
    }
}
