using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class CrazyGamesAds : IAds
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly ICrazyGames _crazyGames;

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
        public bool InterstitialReady => true;
        public bool RewardedReady => true;

        public async UniTask<bool> Initialize()
        {
            SubscribeOnInterstitial();
            SubscribeOnRewarded();
            SubscribeOnCrazyGames();
            BannerLoaded?.Invoke(true);
            InterstitialLoaded?.Invoke(true);
            RewardedLoaded?.Invoke(true, string.Empty);
            return true;
        }

        public bool ShowBanner()
        {
            _logger.Print("Crazy Games Ads: Banner show!");
            _crazyGames.RequestResponsiveBannerAds("ads-bottom-banner-320x50");
            return true;
        }

        public void HideBanner()
        {
            _logger.Print("Crazy Games Ads: Banner hide!");
            _crazyGames.HideBannerAds("ads-bottom-banner-320x50");
        }

        public bool ShowInterstitial()
        {
            _logger.Print("Crazy Games Ads: Interstitial show!");
            _currentVideoShowing = AdsVideo.Interstitial;
            _crazyGames.RequestInterstitialAds();
            return true;
        }

        public bool ShowRewarded(string tag)
        {
            _logger.Print($"Crazy Games Ads: Rewarded {tag} show!");
            _currentVideoShowing = AdsVideo.Rewarded;
            _rewardedTag = tag;
            _crazyGames.RequestRewardedAds();
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

        private void SubscribeOnCrazyGames()
        {
            _crazyGames.AdsBannerShown += OnCrazyGamesAdsBannerShown;
            _crazyGames.AdsBannerHid += OnCrazyGamesAdsBannerHid;
            _crazyGames.AdsBannerError += OnCrazyGamesAdsBannerError;
            _crazyGames.AdsInterstitialStarted += OnCrazyGamesAdsInterstitialStarted;
            _crazyGames.AdsInterstitialError += OnCrazyGamesAdsInterstitialError;
            _crazyGames.AdsInterstitialFinished += OnCrazyGamesAdsInterstitialFinished;
            _crazyGames.AdsRewardedStarted += OnCrazyGamesAdsRewardedStarted;
            _crazyGames.AdsRewardedError += OnCrazyGamesAdsRewardedError;
            _crazyGames.AdsRewardedFinished += OnCrazyGamesAdsRewardedFinished;
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

        private void OnCrazyGamesAdsBannerShown(string containerId)
        {
            _logger.Print($"Crazy Games Ads: Banner \"{containerId}\" shown!");
            BannerShown?.Invoke(true);
        }

        private void OnCrazyGamesAdsBannerHid(string containerId)
        {
            _logger.Print($"Crazy Games Ads: Banner \"{containerId}\" hid!");
            BannerHid?.Invoke();
        }

        private void OnCrazyGamesAdsBannerError(string containerId, string error)
        {
            _logger.Print($"Crazy Games Ads: Banner \"{containerId}\" error \"{error}\"!");
            BannerShown?.Invoke(false);
        }

        private void OnCrazyGamesAdsInterstitialStarted()
        {
            _logger.Print("Crazy Games Ads: Interstitial started!");
            InterstitialOpened?.Invoke(true);
        }

        private void OnCrazyGamesAdsInterstitialError(string error)
        {
            _logger.Print($"Crazy Games Ads: Interstitial error \"{error}\"!");
            InterstitialOpened?.Invoke(false);
        }

        private void OnCrazyGamesAdsInterstitialFinished()
        {
            _logger.Print("Crazy Games Ads: Interstitial finished!");
            InterstitialClosed?.Invoke();
        }

        private void OnCrazyGamesAdsRewardedStarted()
        {
            _logger.Print($"Crazy Games Ads: Rewarded {_rewardedTag} started!");
            RewardedOpened?.Invoke(true, _rewardedTag);
        }

        private void OnCrazyGamesAdsRewardedError(string error)
        {
            _logger.Print($"Crazy Games Ads: Rewarded {_rewardedTag} error \"{error}\"!");
            RewardedOpened?.Invoke(false, _rewardedTag);
        }

        private void OnCrazyGamesAdsRewardedFinished()
        {
            _logger.Print($"Crazy Games Ads: Rewarded {_rewardedTag} finished!");
            RewardedClosed?.Invoke(true, _rewardedTag);
        }
    }
}
