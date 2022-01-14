/*
using System;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
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
#else
        private const string AppKey = "";
#endif

        [Inject]
        private readonly ILogger logger;

        private bool videoAdShowing;

        private string rewardedTag;

        public bool CanShowInterstitial => !videoAdShowing;
        public bool CanShowRewarded => !videoAdShowing;
        public bool HasBanner => Appodeal.isLoaded(Appodeal.BANNER);
        public bool HasInterstitial => Appodeal.isLoaded(Appodeal.INTERSTITIAL);
        public bool HasRewarded => Appodeal.isLoaded(Appodeal.REWARDED_VIDEO);

        public event Action OnInitialized;
        public event Action<bool> OnBannerLoaded;
        public event Action OnBannerShown;
        public event Action<bool> OnInterstitialLoaded;
        public event Action<bool> OnInterstitialOpened;
        public event Action OnInterstitialClosed;
        public event Action<bool, string> OnRewardedLoaded;
        public event Action<bool, string> OnRewardedOpened;
        public event Action<bool, string> OnRewardedClosed;

        public void Init()
        {
            SubscribeOnInterstitial();
            SubscribeOnRewarded();

            Appodeal.initialize(AppKey, Appodeal.BANNER | Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO);

            InitBanner();
            InitInterstitial();
            InitRewarded();

            OnInitialized?.Invoke();
        }

        private void SubscribeOnInterstitial()
        {
            OnInterstitialOpened += _ => videoAdShowing = true;
            OnInterstitialClosed += () => videoAdShowing = false;
        }

        private void SubscribeOnRewarded()
        {
            OnRewardedOpened += (_, __) => videoAdShowing = true;
            OnRewardedClosed += (_, __) => videoAdShowing = false;
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
            if (HasRewarded)
            {
                Appodeal.show(Appodeal.REWARDED_VIDEO);
                rewardedTag = tag;
                return true;
            }
            rewardedTag = string.Empty;
            return false;
        }

        public void onBannerLoaded(int height, bool isPrecache)
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Banner loaded!");
                OnBannerLoaded?.Invoke(true);
            });
        }

        public void onBannerFailedToLoad()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Banner failed to load!");
                OnBannerLoaded?.Invoke(false);
            });
        }

        public void onBannerShown()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Banner shown!");
                OnBannerShown?.Invoke();
            });
        }

        public void onBannerClicked()
        {
            InvokeOnMainThread(() => logger.Print("Banner clicked!"));
        }

        public void onBannerExpired()
        {
            InvokeOnMainThread(() => logger.Print("Banner expired!"));
        }

        public void onInterstitialLoaded(bool isPrecache)
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Interstitial loaded!");
                OnInterstitialLoaded?.Invoke(true);
            });
        }

        public void onInterstitialFailedToLoad()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Interstitial failed to load!");
                OnInterstitialLoaded?.Invoke(false);
            });
        }

        public void onInterstitialShowFailed()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Interstitial show failed!");
                OnInterstitialOpened?.Invoke(false);
            });
        }

        public void onInterstitialShown()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Interstitial shown!");
                OnInterstitialOpened?.Invoke(true);
            });
        }

        public void onInterstitialClosed()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Interstitial closed!");
                OnInterstitialClosed?.Invoke();
            });
        }

        public void onInterstitialClicked()
        {
            InvokeOnMainThread(() => logger.Print("Interstitial clicked!"));
        }

        public void onInterstitialExpired()
        {
            InvokeOnMainThread(() => logger.Print("Interstitial expired!"));
        }

        public void onRewardedVideoLoaded(bool precache)
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Rewarded loaded!");
                OnRewardedLoaded?.Invoke(true, rewardedTag);
            });
        }

        public void onRewardedVideoFailedToLoad()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Rewarded failed to load!");
                OnRewardedLoaded?.Invoke(false, rewardedTag);
            });
        }

        public void onRewardedVideoShowFailed()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Rewarded show failed!");
                OnRewardedOpened?.Invoke(false, rewardedTag);
            });
        }

        public void onRewardedVideoShown()
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Rewarded shown!");
                OnRewardedOpened?.Invoke(true, rewardedTag);
            });
        }

        public void onRewardedVideoFinished(double amount, string name)
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Rewarded finished!");
                OnRewardedClosed?.Invoke(true, rewardedTag);
            });
        }

        public void onRewardedVideoClosed(bool finished)
        {
            InvokeOnMainThread(() =>
            {
                logger.Print("Rewarded closed!");
                OnRewardedClosed?.Invoke(false, rewardedTag);
            });
        }

        public void onRewardedVideoExpired()
        {
            InvokeOnMainThread(() => logger.Print("Rewarded expired!"));
        }

        public void onRewardedVideoClicked()
        {
            InvokeOnMainThread(() => logger.Print("Rewarded clicked!"));
        }

        private static void InvokeOnMainThread(Action action)
        {
            Scheduler.MainThread.Schedule(action);
        }
    }
}
*/
