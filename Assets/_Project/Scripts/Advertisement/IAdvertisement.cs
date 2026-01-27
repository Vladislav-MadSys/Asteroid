using System;

namespace _Project.Scripts.Advertisement
{
    public interface IAdvertisement
    {
        public void ShowRewardedAd(Action callback);
        public void ShowInterstitialAd();
    }
}
