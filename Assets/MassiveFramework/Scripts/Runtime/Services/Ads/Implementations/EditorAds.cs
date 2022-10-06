using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorAds : IAds
    {
        [Inject]
        private readonly ILogger _logger;

        public bool InterstitialShowingAvailable => true;
        public bool RewardedShowingAvailable => true;
        public bool BannerReady => true;
        public bool InterstitialReady => true;
        public bool RewardedReady => true;

        public event Action<bool> OnBannerLoaded;
        public event Action OnBannerShown;
        public event Action<bool> OnInterstitialLoaded;
        public event Action<bool> OnInterstitialOpened;
        public event Action OnInterstitialClosed;
        public event Action<bool, string> OnRewardedLoaded;
        public event Action<bool, string> OnRewardedOpened;
        public event Action<bool, string> OnRewardedClosed;

        public async UniTask<bool> Initialize()
        {
            _logger.Print("EditorAds.Initialize()");
            return true;
        }

        public void ShowBanner()
        {
            _logger.Print("EditorAds.ShowBanner()");
            OnBannerLoaded?.Invoke(true);
            OnBannerShown?.Invoke();
        }

        public void ShowInterstitial()
        {
            _logger.Print("EditorAds.ShowInterstitial()");
            OnInterstitialLoaded?.Invoke(true);
            OnInterstitialOpened?.Invoke(true);
            OnInterstitialClosed?.Invoke();
        }

        public bool ShowRewarded(string tag)
        {
            _logger.Print($"EditorAds.ShowRewarded(\"{tag}\")");
            OnRewardedLoaded?.Invoke(true, tag);
            OnRewardedOpened?.Invoke(true, tag);
            OnRewardedClosed?.Invoke(true, tag);
            return true;
        }
    }
}
