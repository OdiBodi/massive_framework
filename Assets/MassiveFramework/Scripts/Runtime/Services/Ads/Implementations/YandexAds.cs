using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class YandexAds : IAds
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IYandex _yandex;

        private AdsVideo _currentVideoShowing;

        private string _rewardedTag;

        public bool InterstitialAvailable => _currentVideoShowing == AdsVideo.None;
        public bool RewardedAvailable => _currentVideoShowing == AdsVideo.None;
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
            _logger.Print("Yandex Ads: Banner show!");
            _yandex.ShowBannerAds();
            BannerShown?.Invoke(true);
            return true;
        }

        public void HideBanner()
        {
            _logger.Print("Yandex Ads: Banner hide!");
            _yandex.HideBannerAds();
            BannerHid?.Invoke();
        }

        public bool ShowInterstitial()
        {
            _logger.Print("Yandex Ads: Interstitial show!");
            _currentVideoShowing = AdsVideo.Interstitial;
            _yandex.ShowInterstitialAds();
            return true;
        }

        public bool ShowRewarded(string tag)
        {
            _logger.Print("Yandex Ads: Rewarded show!");
            _currentVideoShowing = AdsVideo.Rewarded;
            _rewardedTag = tag;
            _yandex.ShowRewardedAds();
            return true;
        }

        private void SubscribeOnInterstitial()
        {
            InterstitialOpened += result => _currentVideoShowing = result ? AdsVideo.Interstitial : AdsVideo.None;
            InterstitialClosed += ResetInterstitial;
        }

        private void SubscribeOnRewarded()
        {
            RewardedOpened += (result, _) => _currentVideoShowing = result ? AdsVideo.Rewarded : AdsVideo.None;
            RewardedClosed += (result, _) =>
            {
                if (result)
                {
                    return;
                }
                ResetRewarded();
            };
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

        private void ResetInterstitial()
        {
            _currentVideoShowing = AdsVideo.None;
        }

        private void ResetRewarded()
        {
            _currentVideoShowing = AdsVideo.None;
            _rewardedTag = string.Empty;
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
        }

        private void OnYandexRewardedAdsError(string error)
        {
            _logger.PrintError($"Yandex Ads: Rewarded error: {error}!");
        }
    }
}
