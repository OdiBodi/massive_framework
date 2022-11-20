using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class YandexAds : IAds
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IYandex _yandex;

        private bool _videoAdShowing;

        private string _rewardedTag;

        public bool InterstitialAvailable => !_videoAdShowing;
        public bool RewardedAvailable => !_videoAdShowing;
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
            SubscribeOnInterstitial();
            SubscribeOnRewarded();
            SubscribeOnYandexInterstitial();
            SubscribeOnYandexRewarded();
            BannerLoaded?.Invoke(true);
            InterstitialLoaded?.Invoke(true);
            RewardedLoaded?.Invoke(true, _rewardedTag);
            return true;
        }

        public bool ShowBanner()
        {
            _yandex.ShowBannerAds();
            BannerShown?.Invoke(true);
            return true;
        }

        public bool ShowInterstitial()
        {
            _videoAdShowing = true;
            _yandex.ShowInterstitialAds();
            return true;
        }

        public bool ShowRewarded(string tag)
        {
            _videoAdShowing = true;
            _rewardedTag = tag;
            _yandex.ShowRewardedAds();
            return true;
        }

        private void SubscribeOnInterstitial()
        {
            InterstitialOpened += result => _videoAdShowing = result;
            InterstitialClosed += () => _videoAdShowing = false;
        }

        private void SubscribeOnRewarded()
        {
            RewardedOpened += (result, _) => _videoAdShowing = result;
            RewardedClosed += (_, _) => _videoAdShowing = false;
        }

        private void SubscribeOnYandexInterstitial()
        {
            _yandex.InterstitialAdsOpened += OnYandexInterstitialAdsOpened;
            _yandex.InterstitialAdsClosed += OnYandexInterstitialAdsClosed;
            _yandex.InterstitialAdsError += OnYandexInterstitialAdsError;
        }

        private void SubscribeOnYandexRewarded()
        {
            _yandex.RewardedAdsOpened += OnYandexRewardedAdsOpened;
            _yandex.RewardedAdsRewarded += OnYandexRewardedAdsRewarded;
            _yandex.RewardedAdsClosed += OnYandexRewardedAdsClosed;
            _yandex.RewardedAdsError += OnYandexRewardedAdsError;
        }

        private void OnYandexInterstitialAdsOpened()
        {
            _logger.Print("Yandex Ads: Interstitial opened!");
            InterstitialOpened?.Invoke(true);
        }

        private void OnYandexInterstitialAdsClosed(bool wasShown)
        {
            if (wasShown)
            {
                _logger.Print("Yandex Ads: Interstitial closed!");
                InterstitialClosed?.Invoke();
            }
            else
            {
                _logger.PrintError("Yandex Ads: Interstitial opened with error!");
                InterstitialOpened?.Invoke(false);
            }
        }

        private void OnYandexInterstitialAdsError(string error)
        {
            _logger.PrintError($"Yandex Ads: Interstitial error: {error}!");
        }
        
        private void OnYandexRewardedAdsOpened()
        {
            _logger.Print("Yandex Ads: Rewarded opened!");
            RewardedOpened?.Invoke(true, _rewardedTag);
        }
        
        private void OnYandexRewardedAdsRewarded()
        {
            _logger.Print("Yandex Ads: Rewarded rewarded!");
            RewardedClosed?.Invoke(true, _rewardedTag);
        }

        private void OnYandexRewardedAdsClosed()
        {
            _logger.Print("Yandex Ads: Rewarded rewarded!");
            RewardedClosed?.Invoke(false, _rewardedTag);
            _rewardedTag = string.Empty;
        }

        private void OnYandexRewardedAdsError(string error)
        {
            _logger.PrintError($"Yandex Ads: Rewarded error: {error}!");
        }
    }
}
