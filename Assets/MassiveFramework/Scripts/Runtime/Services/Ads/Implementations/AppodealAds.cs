/*
using System;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace MassiveCore.Framework
{
    public class AppodealAds : IAds, IBannerAdListener, IInterstitialAdListener, IRewardedVideoAdListener
    {
#if UNITY_IOS
        private const string AppKey = "";
#elif UNITY_ANDROID
        private const string AppKey = "";
#endif

        [Inject]
        private readonly ILogger _logger;

        private bool _videoAdShowing;

        private string _rewardedTag;

        public bool InterstitialShowingAvailable => !_videoAdShowing;
        public bool RewardedShowingAvailable => !_videoAdShowing;
        public bool BannerReady => Appodeal.isLoaded(Appodeal.BANNER);
        public bool InterstitialReady => Appodeal.isLoaded(Appodeal.INTERSTITIAL);
        public bool RewardedReady => Appodeal.isLoaded(Appodeal.REWARDED_VIDEO);

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
            SubscribeOnInterstitial();
            SubscribeOnRewarded();

            Appodeal.initialize(AppKey, Appodeal.BANNER | Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);

            InitBanner();
            InitInterstitial();
            InitRewarded();

            return true;
        }

        private void SubscribeOnInterstitial()
        {
            OnInterstitialOpened += _ => _videoAdShowing = true;
            OnInterstitialClosed += () => _videoAdShowing = false;
        }

        private void SubscribeOnRewarded()
        {
            OnRewardedOpened += (_, __) => _videoAdShowing = true;
            OnRewardedClosed += (_, __) => _videoAdShowing = false;
        }

        private void InitBanner()
        {
            Appodeal.setBannerBackground(false);
            Appodeal.setBannerCallbacks(this);
        }
        
        private void InitInterstitial()
        {
            Appodeal.setInterstitialCallbacks(this);
        }
        
        private void InitRewarded()
        {
            Appodeal.setRewardedVideoCallbacks(this);
        }

        public void ShowBanner()
        {
            Appodeal.show(Appodeal.BANNER_BOTTOM);
        }

        public void ShowInterstitial()
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }

        public bool ShowRewarded(string tag)
        {
            if (Appodeal.show(Appodeal.REWARDED_VIDEO))
            {
                _rewardedTag = tag;
                return true;
            }
            _rewardedTag = string.Empty;
            return false;
        }

        public void onBannerLoaded(int height, bool isPrecache)
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Banner loaded!");
                OnBannerLoaded?.Invoke(true);
            });
        }

        public void onBannerFailedToLoad()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Banner failed to load!");
                OnBannerLoaded?.Invoke(false);
            });
        }

        public void onBannerShown()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Banner shown!");
                OnBannerShown?.Invoke();
            });
        }

        public void onBannerClicked()
        {
            InvokeOnMainThread(() => _logger.Print("Banner clicked!"));
        }

        public void onBannerExpired()
        {
            InvokeOnMainThread(() => _logger.Print("Banner expired!"));
        }

        public void onInterstitialLoaded(bool isPrecache)
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Interstitial loaded!");
                OnInterstitialLoaded?.Invoke(true);
            });
        }

        public void onInterstitialFailedToLoad()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Interstitial failed to load!");
                OnInterstitialLoaded?.Invoke(false);
            });
        }

        public void onInterstitialShowFailed()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Interstitial show failed!");
                OnInterstitialOpened?.Invoke(false);
            });
        }

        public void onInterstitialShown()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Interstitial shown!");
                OnInterstitialOpened?.Invoke(true);
            });
        }

        public void onInterstitialClosed()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Interstitial closed!");
                OnInterstitialClosed?.Invoke();
            });
        }

        public void onInterstitialClicked()
        {
            InvokeOnMainThread(() => _logger.Print("Interstitial clicked!"));
        }

        public void onInterstitialExpired()
        {
            InvokeOnMainThread(() => _logger.Print("Interstitial expired!"));
        }

        public void onRewardedVideoLoaded(bool precache)
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Rewarded loaded!");
                OnRewardedLoaded?.Invoke(true, _rewardedTag);
            });
        }

        public void onRewardedVideoFailedToLoad()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Rewarded failed to load!");
                OnRewardedLoaded?.Invoke(false, _rewardedTag);
            });
        }

        public void onRewardedVideoShowFailed()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Rewarded show failed!");
                OnRewardedOpened?.Invoke(false, _rewardedTag);
            });
        }

        public void onRewardedVideoShown()
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Rewarded shown!");
                OnRewardedOpened?.Invoke(true, _rewardedTag);
            });
        }

        public void onRewardedVideoFinished(double amount, string name)
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Rewarded finished!");
                OnRewardedClosed?.Invoke(true, _rewardedTag);
            });
        }

        public void onRewardedVideoClosed(bool finished)
        {
            InvokeOnMainThread(() =>
            {
                _logger.Print("Rewarded closed!");
                OnRewardedClosed?.Invoke(false, _rewardedTag);
            });
        }

        public void onRewardedVideoExpired()
        {
            InvokeOnMainThread(() => _logger.Print("Rewarded expired!"));
        }

        public void onRewardedVideoClicked()
        {
            InvokeOnMainThread(() => _logger.Print("Rewarded clicked!"));
        }

        private static void InvokeOnMainThread(Action action)
        {
            Scheduler.MainThread.Schedule(action);
        }
    }
}
*/