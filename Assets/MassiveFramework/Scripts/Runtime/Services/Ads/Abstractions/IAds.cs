using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IAds
    {
        event Action<bool> BannerLoaded;
        event Action<bool> BannerShown;
        event Action<bool> InterstitialLoaded;
        event Action<bool> InterstitialOpened;
        event Action InterstitialClosed;
        event Action<bool, string> RewardedLoaded;
        event Action<bool, string> RewardedOpened;
        event Action<bool, string> RewardedClosed;

        bool InterstitialShowingAvailable { get; }
        bool RewardedShowingAvailable { get; }
        bool BannerReady { get; }
        bool InterstitialReady { get; }
        bool RewardedReady { get; }

        UniTask<bool> Initialize();
        bool ShowBanner();
        bool ShowInterstitial();
        bool ShowRewarded(string tag);
    }
}
