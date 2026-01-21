using System;

public interface IAdvertisement
{
    public void ShowRewardedAd(Action callback);
    public void ShowInterstitialAd();
}
