using System;
using _Project.Scripts.Saves;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Purchases.Purchasing
{
    public class PurchasingStab : IPurchaser, IInitializable, IDisposable
    {
        private const string REMOVE_ADS_PURCHASE_KEY = "com.DefaultCompany.Asteroid.removeAds";
        private const string BUY_100_RANDOM_POINTS_KEY = "com.DefaultCompany.Asteroid.100RandomConsumablePoints";
        
        public bool IsAdsRemoved { get; private set; } = false;

        private ISaveSystem _saveSystem;
        
        public PurchasingStab(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }
        public void Initialize()
        {
            _saveSystem.OnSaveLoaded += OnLocalSaveDataLoaded;
        }

        public void Dispose()
        {
            _saveSystem.OnSaveLoaded -= OnLocalSaveDataLoaded;
        }
        
        public void OnPurchaseButtonClicked(string productId)
        {
            OnSuccessBuying(productId);
        }

        public void OnSuccessBuying(string productId)
        {
            switch (productId)
            {
                case REMOVE_ADS_PURCHASE_KEY:
                    RemoveAds();
                    break;
                case BUY_100_RANDOM_POINTS_KEY:
                    RandomPoints();
                    break;
            }
        }
        private void OnLocalSaveDataLoaded(SaveData saveData)
        {
            if (saveData != null && IsAdsRemoved != true)
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
