using static IronSource;
using UnityEngine;
using com.unity3d.mediation;

public class IronSourceInterstitial : MonoBehaviour
{
    private LevelPlayInterstitialAd interstitialAd;
    void CreateInterstitialAd()
    {
        //Create InterstitialAd instance
        interstitialAd = new LevelPlayInterstitialAd("interstitialAdUnitId");

        //Subscribe InterstitialAd events
        interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;
    }
    void LoadInterstitialAd()
    {
        //Load or reload InterstitialAd 	
        interstitialAd.LoadAd();
    }
    void ShowInterstitialAd()
    {
        //Show InterstitialAd, check if the ad is ready before showing
        if (interstitialAd.IsAdReady())
        {
            interstitialAd.ShowAd();
        }
    }

    void DestroyInterstitialAd()
    {
        //Destroy InterstitialAd 
        interstitialAd.DestroyAd();
    }

    //Implement InterstitialAd events
    void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError) { }
    void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo) { }
    void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo) { }
}