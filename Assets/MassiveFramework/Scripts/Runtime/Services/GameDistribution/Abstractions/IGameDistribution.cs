using System;

namespace MassiveCore.Framework
{
    public interface IGameDistribution
    {
        event Action<bool> Initialized;
        event Action Resumed;
        event Action Paused;
        event Action<string> AdsBannerShown;
        event Action<string> AdsBannerHid;
        event Action<string, string> AdsBannerError;
        event Action AdsVideoLoaded;
        event Action AdsVideoOpened;
        event Action AdsVideoSkipped;
        event Action AdsVideoComplete;
        event Action<bool> AdsInterstitialPreloaded;
        event Action<bool> AdsRewardedPreloaded;
        event Action AdsInterstitialFinished;
        event Action AdsRewardedFinished;

        void Initialize();
        void ShowConsole();
        void PreloadInterstitialAds();
        void PreloadRewardedAds();
        void ShowBannerAds(string containerId);
        void HideBannerAds(string containerId);
        void ShowInterstitialAds();
        void ShowRewardedAds();
    }
}
