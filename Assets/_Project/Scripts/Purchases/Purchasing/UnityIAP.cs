using System;
using _Project.Scripts.Saves;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Purchases.Purchasing
{
    public class UnityIAP : IPurchaser, IInitializable, IDisposable
    {
        private const string REMOVE_ADS_PURCHASE_KEY = "com.DefaultCompany.Asteroid.removeAds";
        private const string BUY_100_RANDOM_POINTS_KEY = "com.DefaultCompany.Asteroid.100RandomConsumablePoints";
        
        public bool IsAdsRemoved { get; private set; } = false;
        
        private StoreController _storeController; 
        private ISaveSystem _saveSystem;
    
        private UnityIAP(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public async void Initialize()
        {
            _storeController = UnityIAPServices.StoreController();
            await _storeController.Connect();

            _storeController.OnPurchasePending += OnPurchasePending;
            _storeController.OnPurchasesFetched += OnPurchasesFetched;
            _saveSystem.OnSaveLoaded += OnLocalSaveDataLoaded;
        }

        public void Dispose()
        {
            _storeController.OnPurchasePending -= OnPurchasePending;
            _storeController.OnPurchasesFetched -= OnPurchasesFetched;
            _saveSystem.OnSaveLoaded -= OnLocalSaveDataLoaded;
        }

        public void OnPurchaseButtonClicked(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                Debug.LogError("IAPButton productId is empty");
                return;
            }
            else if (!CodelessIAPStoreListener.Instance.HasProductInCatalog(productId!))
            {
                Debug.LogWarning("The product catalog has no product with the ID \"" + productId + "\"");
                return;
            }
            CodelessIAPStoreListener.Instance.InitiatePurchase(productId);
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

        private void OnLocalSaveDataLoaded(SaveData saveData)
        {
            if (saveData != null)
            {
                IsAdsRemoved = saveData.isAdsRemoved;
            }
        }

        private void RemoveAds()
        {
            IsAdsRemoved = true;
            _saveSystem.Save();
            Debug.Log("Removing Ads");
        }

        private void RandomPoints()
        {
            Debug.Log("WOW! You bought 100 random points!");
        }
    }
}
