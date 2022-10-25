/*
using System;
using System.Collections.Generic;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace MassiveCore.Framework
{
    public class AppodealAds : IAds, IAppodealInitializationListener, IBannerAdListener, IInterstitialAdListener,
        IRewardedVideoAdListener
    {
#if UNITY_IOS
        private const string AppKey = "";
#elif UNITY_ANDROID
        private const string AppKey = "";
#endif

        [Inject]
        private readonly ILogger _logger;

        private readonly ReactiveProperty<bool> _initialized = new(); 
        
        private bool _videoAdShowing;

        private string _rewardedTag;

        public bool InterstitialAvailable => !_videoAdShowing;
        public bool RewardedAvailable => !_videoAdShowing;
        public bool BannerReady => Appodeal.IsLoaded(AppodealAdType.Banner);
        public bool InterstitialReady => Appodeal.IsLoaded(AppodealAdType.Interstitial);
        public bool RewardedReady => Appodeal.IsLoaded(AppodealAdType.RewardedVideo);

        public event Action<bool> BannerLoaded;
        public event Action<bool> BannerShown;
        public event Action<bool> InterstitialLoaded;
        public event Action<bool> InterstitialOpened;
        public event Action InterstitialClosed;
        public event Action<bool, string> RewardedLoaded;
        public event Action<bool, string> RewardedOpened;
        public event Action<bool, string> RewardedClosed;

        public UniTask<bool> Initialize()
        {
            SubscribeOnInterstitial();
            SubscribeOnRewarded();

            Appodeal.SetLogLevel(AppodealLogLevel.Verbose);

            InitializeBanner();
            InitializeInterstitial();
            InitializeRewarded();

            const int adTypes = AppodealAdType.Banner | AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;
            Appodeal.Initialize(AppKey, adTypes, this);

            return _initialized.WaitUntilValueChangedAsync().AsUniTask();
        }

        private void SubscribeOnInterstitial()
        {
            InterstitialOpened += _ => _videoAdShowing = true;
            InterstitialClosed += () => _videoAdShowing = false;
        }

        private void SubscribeOnRewarded()
        {
            RewardedOpened += (_, __) => _videoAdShowing = true;
            RewardedClosed += (_, __) => _videoAdShowing = false;
        }

        private void InitializeBanner()
        {
            Appodeal.SetBannerCallbacks(this);
        }

        private void InitializeInterstitial()
        {
            Appodeal.SetInterstitialCallbacks(this);
        }
        
        private void InitializeRewarded()
        {
            Appodeal.SetRewardedVideoCallbacks(this);
        }

        public bool ShowBanner()
        {
            var result = Appodeal.Show(AppodealShowStyle.BannerBottom);
            return result;
        }

        public bool ShowInterstitial()
        {
            var result = Appodeal.Show(AppodealShowStyle.Interstitial);
            return result;
        }

        public bool ShowRewarded(string tag)
        {
            var result = Appodeal.Show(AppodealShowStyle.RewardedVideo); 
            _rewardedTag = result ? tag : string.Empty;
            return result;
        }

        public void OnInitializationFinished(List<string> errors)
        {
            if (errors == null)
            {
                _logger.Print("Appodeal Ads: Initialized!");
            }
            else
            {
                _logger.PrintError($"Appodeal Ads: Initialized with errors: {errors}");
            }
            _initialized.Value = errors == null;
        }

        public void OnBannerLoaded(int height, bool isPrecache)
        {
            _logger.Print("Appodeal Ads: Banner loaded!");
            BannerLoaded?.Invoke(true);
        }

        public void OnBannerFailedToLoad()
        {
            _logger.PrintError("Appodeal Ads: Banner failed to load!");
            BannerLoaded?.Invoke(false);
        }

        public void OnBannerShown()
        {
            _logger.Print("Appodeal Ads: Banner shown!");
            BannerShown?.Invoke(true);
        }

        public void OnBannerShowFailed()
        {
            _logger.PrintError("Appodeal Ads: Banner show failed!");
            BannerShown?.Invoke(false);
        }

        public void OnBannerClicked()
        {
            _logger.Print("Appodeal Ads: Banner clicked!");
        }

        public void OnBannerExpired()
        {
            _logger.Print("Appodeal Ads: Banner expired!");
        }

        public void OnInterstitialLoaded(bool isPrecache)
        {
            _logger.Print("Appodeal Ads: Interstitial loaded!");
            InterstitialLoaded?.Invoke(true);
        }

        public void OnInterstitialFailedToLoad()
        {
            _logger.PrintError("Appodeal Ads: Interstitial failed to load!");
            InterstitialLoaded?.Invoke(false);
        }

        public void OnInterstitialShowFailed()
        {
            _logger.PrintError("Appodeal Ads: Interstitial show failed!");
            InterstitialOpened?.Invoke(false);
        }

        public void OnInterstitialShown()
        {
            _logger.Print("Appodeal Ads: Interstitial shown!");
            InterstitialOpened?.Invoke(true);
        }

        public void OnInterstitialClosed()
        {
            _logger.Print("Appodeal Ads: Interstitial closed!");
            InterstitialClosed?.Invoke();
        }

        public void OnInterstitialClicked()
        {
            _logger.Print("Appodeal Ads: Interstitial clicked!");
        }

        public void OnInterstitialExpired()
        {
            _logger.Print("Appodeal Ads: Interstitial expired!");
        }

        public void OnRewardedVideoLoaded(bool precache)
        {
            _logger.Print("Appodeal Ads: Rewarded loaded!");
            RewardedLoaded?.Invoke(true, _rewardedTag);
        }

        public void OnRewardedVideoFailedToLoad()
        {
            _logger.PrintError("Appodeal Ads: Rewarded failed to load!");
            RewardedLoaded?.Invoke(false, _rewardedTag);
        }

        public void OnRewardedVideoShowFailed()
        {
            _logger.PrintError("Appodeal Ads: Rewarded show failed!");
            RewardedOpened?.Invoke(false, _rewardedTag);
        }

        public void OnRewardedVideoShown()
        {
            _logger.Print("Appodeal Ads: Rewarded shown!");
            RewardedOpened?.Invoke(true, _rewardedTag);
        }

        public void OnRewardedVideoFinished(double amount, string name)
        {
            _logger.Print("Appodeal Ads: Rewarded finished!");
            RewardedClosed?.Invoke(true, _rewardedTag);
        }

        public void OnRewardedVideoClosed(bool finished)
        {
            _logger.Print("Appodeal Ads: Rewarded closed!");
            RewardedClosed?.Invoke(false, _rewardedTag);
        }

        public void OnRewardedVideoExpired()
        {
            _logger.Print("Appodeal Ads: Rewarded expired!");
        }

        public void OnRewardedVideoClicked()
        {
            _logger.Print("Appodeal Ads: Rewarded clicked!");
        }
    }
}
*/