using System;

namespace MassiveCore.Framework
{
    public interface IAds
    {
        event Action OnInitialized;
        event Action<bool> OnBannerLoaded;
        event Action OnBannerShown;
        event Action<bool> OnInterstitialLoaded;
        event Action<bool> OnInterstitialOpened;
        event Action OnInterstitialClosed;
        event Action<bool, string> OnRewardedLoaded;
        event Action<bool, string> OnRewardedOpened;
        event Action<bool, string> OnRewardedClosed;

        bool CanShowInterstitial { get; }
        bool CanShowRewarded { get; }
        bool HasBanner { get; }
        bool HasInterstitial { get; }
        bool HasRewarded { get; }

        void Init();
        void ShowBanner();
        void ShowInterstitial();
        bool ShowRewarded(string tag);
    }
}
