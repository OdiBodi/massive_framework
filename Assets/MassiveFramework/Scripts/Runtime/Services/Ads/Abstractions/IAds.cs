using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IAds
    {
        event Action<bool> OnBannerLoaded;
        event Action OnBannerShown;
        event Action<bool> OnInterstitialLoaded;
        event Action<bool> OnInterstitialOpened;
        event Action OnInterstitialClosed;
        event Action<bool, string> OnRewardedLoaded;
        event Action<bool, string> OnRewardedOpened;
        event Action<bool, string> OnRewardedClosed;

        bool InterstitialShowingAvailable { get; }
        bool RewardedShowingAvailable { get; }
        bool BannerReady { get; }
        bool InterstitialReady { get; }
        bool RewardedReady { get; }

        UniTask<bool> Initialize();
        void ShowBanner();
        void ShowInterstitial();
        bool ShowRewarded(string tag);
    }
}
