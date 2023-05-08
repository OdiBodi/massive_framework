using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class GameDistributionAds : IAds
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IGameDistribution _gameDistribution;

        private AdsVideo _currentVideoShowing;

        private string _rewardedTag;

        public event Action<bool> BannerLoaded;
        public event Action<bool> BannerShown;
        public event Action BannerHid;
        public event Action<bool> InterstitialLoaded;
        public event Action<bool> InterstitialOpened;
        public event Action InterstitialClosed;
        public event Action<bool, string> RewardedLoaded;
        public event Action<bool, string> RewardedOpened;
        public event Action<bool, string> RewardedClosed;

        public bool InterstitialAvailable => _currentVideoShowing == AdsVideo.None;
        public bool RewardedAvailable => _currentVideoShowing == AdsVideo.None;
        public bool BannerReady => true;
        public bool InterstitialReady { get; private set; }
        public bool RewardedReady { get; private set; }

        public async UniTask<bool> Initialize()
        {
            SubscribeOnInterstitial();
            SubscribeOnRewarded();
            SubscribeOnGameDistribution();
            BannerLoaded?.Invoke(true);
            _gameDistribution.PreloadInterstitialAds();
            _gameDistribution.PreloadRewardedAds();
            await UniTask.WaitUntil(() => BannerReady && InterstitialReady && RewardedReady);
            return true;
        }

        public bool ShowBanner()
        {
            _logger.Print("Game Distribution Ads: Banner show!");
            _gameDistribution.ShowBannerAds("ads-bottom-banner-728x90");
            return true;
        }

        public void HideBanner()
        {
            _logger.Print("Game Distribution Ads: Banner hide!");
            _gameDistribution.HideBannerAds("ads-bottom-banner-728x90");
        }

        public bool ShowInterstitial()
        {
            _logger.Print("Game Distribution Ads: Interstitial show!");
            _currentVideoShowing = AdsVideo.Interstitial;
            _gameDistribution.ShowInterstitialAds();
            return true;
        }

        public bool ShowRewarded(string tag)
        {
            _logger.Print("Game Distribution Ads: Rewarded show!");
            _currentVideoShowing = AdsVideo.Rewarded;
            _rewardedTag = tag;
            _gameDistribution.ShowRewardedAds();
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
            RewardedClosed += (_, _) => ResetRewarded();
        }

        private void SubscribeOnGameDistribution()
        {
            _gameDistribution.AdsBannerShown += OnGameDistributionAdsBannerShown;
            _gameDistribution.AdsBannerHid += OnGameDistributionAdsBannerHid;
            _gameDistribution.AdsBannerError += OnGameDistributionAdsBannerError;
            _gameDistribution.AdsVideoLoaded += OnGameDistributionAdsVideoLoaded;
            _gameDistribution.AdsVideoOpened += OnGameDistributionAdsVideoOpened;
            _gameDistribution.AdsVideoSkipped += OnGameDistributionAdsVideoSkipped;
            _gameDistribution.AdsVideoComplete += OnGameDistributionAdsVideoComplete;
            _gameDistribution.AdsInterstitialPreloaded += OnGameDistributionAdsInterstitialPreloaded;
            _gameDistribution.AdsRewardedPreloaded += OnGameDistributionAdsRewardedPreloaded;
            _gameDistribution.AdsInterstitialFinished += OnGameDistributionAdsInterstitialFinished;
            _gameDistribution.AdsRewardedFinished += OnGameDistributionAdsRewardedFinished;
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

        private void ResetPreloadingInterstitialAds()
        {
            InterstitialReady = false;
            _gameDistribution.PreloadInterstitialAds();
        }

        private void ResetPreloadingRewardedAds()
        {
            RewardedReady = false;
            _gameDistribution.PreloadRewardedAds();
        }

        private void OnGameDistributionAdsBannerShown(string containerId)
        {
            _logger.Print($"Game Distribution Ads: Banner \"{containerId}\" shown!");
            BannerShown?.Invoke(true);
        }

        private void OnGameDistributionAdsBannerHid(string containerId)
        {
            _logger.Print($"Game Distribution Ads: Banner \"{containerId}\" hid!");
            BannerHid?.Invoke();
        }

        private void OnGameDistributionAdsBannerError(string containerId, string error)
        {
            _logger.Print($"Game Distribution Ads: Banner \"{containerId}\" error \"{error}\"!");
            BannerShown?.Invoke(false);
        }

        private void OnGameDistributionAdsVideoLoaded()
        {
            if (_currentVideoShowing == AdsVideo.None)
            {
                _currentVideoShowing = AdsVideo.Interstitial;
            }
            _logger.Print($"Game Distribution Ads: {_currentVideoShowing} loaded!");
            if (_currentVideoShowing == AdsVideo.Interstitial)
            {
                InterstitialLoaded?.Invoke(true);
            }
            else if (_currentVideoShowing == AdsVideo.Rewarded)
            {
                RewardedLoaded?.Invoke(true, _rewardedTag);
            }
        }

        private void OnGameDistributionAdsVideoOpened()
        {
            if (_currentVideoShowing == AdsVideo.None)
            {
                _currentVideoShowing = AdsVideo.Interstitial;
            }
            _logger.Print($"Game Distribution Ads: {_currentVideoShowing} opened!");
            if (_currentVideoShowing == AdsVideo.Interstitial)
            {
                InterstitialOpened?.Invoke(true);
            }
            else if (_currentVideoShowing == AdsVideo.Rewarded)
            {
                RewardedOpened?.Invoke(true, _rewardedTag);
            }
        }

        private void OnGameDistributionAdsVideoSkipped()
        {
            if (_currentVideoShowing == AdsVideo.None)
            {
                _currentVideoShowing = AdsVideo.Interstitial;
            }
            _logger.Print($"Game Distribution Ads: {_currentVideoShowing} skipped!");
            if (_currentVideoShowing == AdsVideo.Interstitial)
            {
                InterstitialClosed?.Invoke();
            }
            else if (_currentVideoShowing == AdsVideo.Rewarded)
            {
                RewardedClosed?.Invoke(false, _rewardedTag);
            }
        }

        private void OnGameDistributionAdsVideoComplete()
        {
            if (_currentVideoShowing == AdsVideo.None)
            {
                _currentVideoShowing = AdsVideo.Interstitial;
            }
            _logger.Print($"Game Distribution Ads: {_currentVideoShowing} complete!");
            if (_currentVideoShowing == AdsVideo.Interstitial)
            {
                InterstitialClosed?.Invoke();
            }
            else if (_currentVideoShowing == AdsVideo.Rewarded)
            {
                RewardedClosed?.Invoke(true, _rewardedTag);
            }
        }

        private void OnGameDistributionAdsInterstitialPreloaded(bool result)
        {
            InterstitialReady = result;
            if (!result)
            {
                _gameDistribution.PreloadInterstitialAds();
            }
        }

        private void OnGameDistributionAdsRewardedPreloaded(bool result)
        {
            RewardedReady = result;
            if (!result)
            {
                _gameDistribution.PreloadRewardedAds();
            }
        }

        private void OnGameDistributionAdsInterstitialFinished()
        {
            InterstitialClosed?.Invoke();
            _logger.Print("Game Distribution Ads: Interstitial finished!");
            ResetPreloadingInterstitialAds();
        }

        private void OnGameDistributionAdsRewardedFinished()
        {
            RewardedClosed?.Invoke(false, _rewardedTag);
            _logger.Print("Game Distribution Ads: Rewarded finished!");
            ResetPreloadingRewardedAds();
        }
    }
}
