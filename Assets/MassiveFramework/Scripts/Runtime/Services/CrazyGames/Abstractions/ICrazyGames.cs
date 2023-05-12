using System;

namespace MassiveCore.Framework.Runtime
{
    public interface ICrazyGames
    {
        event Action<string> AdsBannerShown;
        event Action<string> AdsBannerHid;
        event Action<string, string> AdsBannerError;
        event Action AdsInterstitialStarted;
        event Action<string> AdsInterstitialError;
        event Action AdsInterstitialFinished;
        event Action AdsRewardedStarted;
        event Action AdsRewardedFinished;
        event Action<string> AdsRewardedError;

        void LoadingStart();
        void LoadingStop();

        void GameplayStart();
        void GameplayStop();

        void HappyTime();

        void RequestBannerAds(string id, int width, int height);
        void RequestResponsiveBannerAds(string id);
        void HideBannerAds(string id);

        void RequestInterstitialAds();
        void RequestRewardedAds();
    }
}
