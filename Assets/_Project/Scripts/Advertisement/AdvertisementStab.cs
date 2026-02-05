using UnityEngine;
using Action = System.Action;

namespace _Project.Scripts.Advertisement
{
    public class AdvertisementStab : IAdvertisement
    {   
        public void ShowRewardedAd(Action callback)
        {
            callback.Invoke();
            Debug.Log("Show RewardedAd");
        }

        public void ShowInterstitialAd()
        {
            Debug.Log("Show Interstitial");
        }
    }
}
