using System;
using _Project.Scripts.Saves;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Purchases.Purchasing
{
    public class PurchaserUnityIAP : IPurchaser, IInitializable, IDisposable
    {
        private const string REMOVE_ADS_PURCHASE_KEY = "com.DefaultCompany.Asteroid.removeAds";
        private const string BUY_100_RANDOM_POINTS_KEY = "com.DefaultCompany.Asteroid.100RandomConsumablePoints";
        
        public bool IsAdsRemoved { get; protected set; } = false;
        
        private StoreController _storeController; 
        private ISaveService _saveService;
    
        private PurchaserUnityIAP(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public async void Initialize()
        {
            _storeController = UnityIAPServices.StoreController();
            SaveData savedData = _saveService.Load();
            if (savedData != null)
            {
                IsAdsRemoved = savedData.isAdsRemoved;
            }

            await _storeController.Connect();

            _storeController.OnPurchasePending += OnPurchasePending;
            _storeController.OnPurchasesFetched += OnPurchasesFetched;
        }

        public void Dispose()
        {
            _storeController.OnPurchasePending -= OnPurchasePending;
            _storeController.OnPurchasesFetched -= OnPurchasesFetched;
        }
    
        public void OnPurchasePending(PendingOrder order)
        {
            foreach (var cartItem in order.CartOrdered.Items())
            {
                var product = cartItem.Product;
                switch (product.definition.id)
                {
                    case REMOVE_ADS_PURCHASE_KEY:
                        RemoveAds();
                        break;
                    case BUY_100_RANDOM_POINTS_KEY:
                        RandomPoints();
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
                        case BUY_100_RANDOM_POINTS_KEY:
                            RandomPoints();
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

        private void RandomPoints()
        {
            Debug.Log("WOW! You bought 100 random points!");
        }
    }
}
