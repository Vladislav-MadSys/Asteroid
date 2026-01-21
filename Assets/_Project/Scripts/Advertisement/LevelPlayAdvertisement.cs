using System;
using Unity.Services.LevelPlay;
using Zenject;

public class LevelPlayAdvertisement : IAdvertisement, IInitializable, IDisposable
{
    private const string APP_KEY = "24f80f265";
    private const string REWARDED_AD_UNIT_ID = "Rewarded_Android";
    private const string INTERSTITIAL_AD_UNIT_ID = "Rewarded_Android";
    
    private bool isSDKReady = false;

    private LevelPlayRewardedAd _rewardedAd;
    private Action _rewardedCallback;
    
    private LevelPlayInterstitialAd _interstitialAd;
    
    public void Initialize()
    {
        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        
        LevelPlay.Init(APP_KEY);
    }

    public void Dispose()
    {
        LevelPlay.OnInitSuccess -= SdkInitializationCompletedEvent;
        
        _rewardedCallback = null;
        _interstitialAd = null;
        _rewardedCallback = null;
    }

    private void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        isSDKReady = true;
    }

    public void ShowRewardedAd(Action callback)
    {
        if (!isSDKReady) return;

        _rewardedAd = new LevelPlayRewardedAd(REWARDED_AD_UNIT_ID);
        
        _rewardedCallback = callback;
        _rewardedAd.OnAdLoaded += ShowRewardedAdEvent;
        _rewardedAd.OnAdRewarded += GetRewardedOfRewardedAdEvent;
        
        _rewardedAd.LoadAd();
    }

    private void ShowRewardedAdEvent(LevelPlayAdInfo adInfo)
    {
        if (_rewardedAd != null && _rewardedCallback != null)
        {
            _rewardedAd.ShowAd();
        }
    }

    private void GetRewardedOfRewardedAdEvent(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        _rewardedCallback.Invoke();
        _rewardedAd = null;
        _rewardedCallback = null; 
    }

    public void ShowInterstitialAd()
    {
        if (!isSDKReady) return;

        _interstitialAd = new LevelPlayInterstitialAd(INTERSTITIAL_AD_UNIT_ID);
        
        _interstitialAd.OnAdLoaded += ShowInterstitialAdEvent;
        
        _interstitialAd.LoadAd();
    }

    private void ShowInterstitialAdEvent(LevelPlayAdInfo adInfo)
    {
        if (_interstitialAd.IsAdReady())
        {
            _interstitialAd.ShowAd();
        }
    }
}
