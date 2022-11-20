using System;

namespace MassiveCore.Framework
{
    public interface IYandex
    {
        event Action InterstitialAdsOpened;
        event Action<bool> InterstitialAdsClosed;
        event Action<string> InterstitialAdsError;

        event Action RewardedAdsOpened;
        event Action RewardedAdsRewarded;
        event Action RewardedAdsClosed;
        event Action<string> RewardedAdsError;

        void ShowBannerAds();
        void HideBannerAds();
        void ShowInterstitialAds();
        void ShowRewardedAds();
    }
}
