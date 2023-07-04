using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class EditorAds : IAds
    {
        [Inject]
        private readonly ILogger _logger;

        public bool InterstitialAvailable { get; private set; } = true;
        public bool RewardedAvailable { get; private set; } = true;
        public bool BannerReady => true;
        public bool InterstitialReady => true;
        public bool RewardedReady => true;

        public event Action<bool> BannerLoaded;
        public event Action<bool> BannerShown;
        public event Action BannerHid;
        public event Action<bool> InterstitialLoaded;
        public event Action<bool> InterstitialOpened;
        public event Action InterstitialClosed;
        public event Action<bool, string> RewardedLoaded;
        public event Action<bool, string> RewardedOpened;
        public event Action<bool, string> RewardedClosed;

        public async UniTask<bool> Initialize()
        {
            _logger.Print("Editor Ads: Initialized!");
            BannerLoaded?.Invoke(true);
            return true;
        }

        public bool ShowBanner()
        {
            _logger.Print("Editor Ads: Banner shown!");
            BannerShown?.Invoke(true);
            return true;
        }

        public void HideBanner()
        {
            _logger.Print("Editor Ads: Banner hide!");
            BannerHid?.Invoke();
        }

        public bool ShowInterstitial()
        {
            _logger.Print("Editor Ads: Interstitial shown!");
            InterstitialAvailable = false;
            InterstitialLoaded?.Invoke(true);
            InterstitialOpened?.Invoke(true);
            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
            {
                InterstitialClosed?.Invoke();
                InterstitialAvailable = true;
            });
            return true;
        }

        public bool ShowRewarded(string tag)
        {
            _logger.Print($"Editor Ads: Rewarded \"{tag}\" shown!");
            RewardedAvailable = false;
            RewardedLoaded?.Invoke(true, tag);
            RewardedOpened?.Invoke(true, tag);
            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
            {
                RewardedClosed?.Invoke(true, tag);
                RewardedAvailable = true;
            });
            return true;
        }
    }
}
