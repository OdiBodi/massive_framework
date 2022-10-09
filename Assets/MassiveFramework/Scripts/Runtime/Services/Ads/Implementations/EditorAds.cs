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

        public event Action<bool> BannerLoaded;
        public event Action<bool> BannerShown;
        public event Action<bool> InterstitialLoaded;
        public event Action<bool> InterstitialOpened;
        public event Action InterstitialClosed;
        public event Action<bool, string> RewardedLoaded;
        public event Action<bool, string> RewardedOpened;
        public event Action<bool, string> RewardedClosed;

        public async UniTask<bool> Initialize()
        {
            _logger.Print("EditorAds.Initialize()");
            return true;
        }

        public bool ShowBanner()
        {
            _logger.Print("EditorAds.ShowBanner()");
            BannerLoaded?.Invoke(true);
            BannerShown?.Invoke(true);
            return true;
        }

        public bool ShowInterstitial()
        {
            _logger.Print("EditorAds.ShowInterstitial()");
            InterstitialLoaded?.Invoke(true);
            InterstitialOpened?.Invoke(true);
            InterstitialClosed?.Invoke();
            return true;
        }

        public bool ShowRewarded(string tag)
        {
            _logger.Print($"EditorAds.ShowRewarded(\"{tag}\")");
            RewardedLoaded?.Invoke(true, tag);
            RewardedOpened?.Invoke(true, tag);
            RewardedClosed?.Invoke(true, tag);
            return true;
        }
    }
}
