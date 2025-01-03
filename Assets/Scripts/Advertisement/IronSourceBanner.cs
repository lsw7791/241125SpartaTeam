using com.unity3d.mediation;
using static IronSource;
using UnityEngine;

public class IronSourceBanner : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    public void CreateBannerAd()
    {
        //Create banner instance
        bannerAd = new LevelPlayBannerAd("bannerAdUnitId");
        //Subscribe BannerAd events
        bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
        bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
        bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
        bannerAd.OnAdDisplayFailed += BannerOnAdDisplayFailedEvent;
        bannerAd.OnAdClicked += BannerOnAdClickedEvent;
        bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
        bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
        bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;
    }
    public void LoadBannerAd()
    {
        //Load the banner ad 
        bannerAd.LoadAd();
    }
    public void ShowBannerAd()
    {
        //Show the banner ad, call this method only if you turned off the auto show when you created this banner instance.
        bannerAd.ShowAd();
    }
    public void HideBannerAd()
    {
        //Hide banner
        bannerAd.HideAd();
    }
    public void DestroyBannerAd()
    {
        //Destroy banner
        bannerAd.DestroyAd();
    }
    //Implement BannAd Events
    void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
    void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError) { }
    void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) { }
    void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) { }
}