/*
#if UNITY_IOS || UNITY_ANDROID

using System;
using System.Collections.Generic;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace MassiveCore.Framework.Runtime
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

        private readonly AsyncSubject<bool> _initializerSubject = new(); 

        private AdsVideo _currentVideoShowing;

        private string _rewardedTag;

        public bool InterstitialAvailable => _currentVideoShowing == AdsVideo.None;
        public bool RewardedAvailable => _currentVideoShowing == AdsVideo.None;
        public bool BannerReady => Appodeal.IsLoaded(AppodealAdType.Banner);
        public bool InterstitialReady => Appodeal.IsLoaded(AppodealAdType.Interstitial);
        public bool RewardedReady => Appodeal.IsLoaded(AppodealAdType.RewardedVideo);

        public event Action<bool> BannerLoaded;
        public event Action<bool> BannerShown;
        public event Action BannerHid;
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

            return _initializerSubject.ToUniTask();
        }

        private void CompleteInitialize(bool result)
        {
            _initializerSubject.OnNext(result);
            _initializerSubject.OnCompleted();
        }

        private void SubscribeOnInterstitial()
        {
            InterstitialOpened += result => _currentVideoShowing = result ? AdsVideo.Interstitial : AdsVideo.None;
            InterstitialClosed += ResetInterstitial;
        }

        private void SubscribeOnRewarded()
        {
            RewardedOpened += (result, __) => _currentVideoShowing = result ? AdsVideo.Rewarded : AdsVideo.None;
            RewardedClosed += (_, __) => ResetRewarded();
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
            _logger.Print("Appodeal Ads: Banner show!");
            var result = Appodeal.Show(AppodealShowStyle.BannerBottom);
            return result;
        }

        public void HideBanner()
        {
            _logger.Print("Appodeal Ads: Banner hide!");
            Appodeal.Hide(AppodealAdType.Banner);
            BannerHid?.Invoke();
        }

        public bool ShowInterstitial()
        {
            _logger.Print("Appodeal Ads: Interstitial show!");
            _currentVideoShowing = AdsVideo.Interstitial;
            var result = Appodeal.Show(AppodealShowStyle.Interstitial);
            return result;
        }

        public bool ShowRewarded(string tag)
        {
            _logger.Print("Appodeal Ads: Rewarded show!");
            _currentVideoShowing = AdsVideo.Rewarded;
            var result = Appodeal.Show(AppodealShowStyle.RewardedVideo); 
            _rewardedTag = result ? tag : string.Empty;
            return result;
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

        public void OnInitializationFinished(List<string> errors)
        {
            ScheduleOnMainThread(() =>
            {
                if (errors == null)
                {
                    _logger.Print("Appodeal Ads: Initialized!");
                }
                else
                {
                    _logger.PrintError($"Appodeal Ads: Initialized with errors: {errors}");
                }

                var result = errors == null;
                CompleteInitialize(result);
            });
        }

        public void OnBannerLoaded(int height, bool isPrecache)
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Banner loaded!");
                BannerLoaded?.Invoke(true);
            });
        }

        public void OnBannerFailedToLoad()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.PrintError("Appodeal Ads: Banner failed to load!");
                BannerLoaded?.Invoke(false);
            });
        }

        public void OnBannerShown()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Banner shown!");
                BannerShown?.Invoke(true);
            });
        }

        public void OnBannerShowFailed()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.PrintError("Appodeal Ads: Banner show failed!");
                BannerShown?.Invoke(false);
            });
        }

        public void OnBannerClicked()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Banner clicked!");
            });
        }

        public void OnBannerExpired()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Banner expired!");
            });
        }

        public void OnInterstitialLoaded(bool isPrecache)
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Interstitial loaded!");
                InterstitialLoaded?.Invoke(true);
            });
        }

        public void OnInterstitialFailedToLoad()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.PrintError("Appodeal Ads: Interstitial failed to load!");
                InterstitialLoaded?.Invoke(false);
            });
        }

        public void OnInterstitialShowFailed()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.PrintError("Appodeal Ads: Interstitial show failed!");
                InterstitialOpened?.Invoke(false);
            });
        }

        public void OnInterstitialShown()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Interstitial shown!");
                InterstitialOpened?.Invoke(true);
            });
        }

        public void OnInterstitialClosed()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Interstitial closed!");
                InterstitialClosed?.Invoke();
            });
        }

        public void OnInterstitialClicked()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Interstitial clicked!");
            });
        }

        public void OnInterstitialExpired()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Interstitial expired!");
            });
        }

        public void OnRewardedVideoLoaded(bool precache)
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Rewarded loaded!");
                RewardedLoaded?.Invoke(true, _rewardedTag);
            });
        }

        public void OnRewardedVideoFailedToLoad()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.PrintError("Appodeal Ads: Rewarded failed to load!");
                RewardedLoaded?.Invoke(false, _rewardedTag);
            });
        }

        public void OnRewardedVideoShowFailed()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.PrintError("Appodeal Ads: Rewarded show failed!");
                RewardedOpened?.Invoke(false, _rewardedTag);
            });
        }

        public void OnRewardedVideoShown()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Rewarded shown!");
                RewardedOpened?.Invoke(true, _rewardedTag);
            });
        }

        public void OnRewardedVideoFinished(double amount, string name)
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Rewarded finished!");
                RewardedClosed?.Invoke(true, _rewardedTag);
            });
        }

        public void OnRewardedVideoClosed(bool finished)
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Rewarded closed!");
                RewardedClosed?.Invoke(false, _rewardedTag);
            });
        }

        public void OnRewardedVideoExpired()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Rewarded expired!");
            });
        }

        public void OnRewardedVideoClicked()
        {
            ScheduleOnMainThread(() =>
            {
                _logger.Print("Appodeal Ads: Rewarded clicked!");
            });
        }

        private static void ScheduleOnMainThread(Action action)
        {
            Scheduler.MainThread.Schedule(action);
        }
    }
}

#endif // UNITY_IOS || UNITY_ANDROID
*/