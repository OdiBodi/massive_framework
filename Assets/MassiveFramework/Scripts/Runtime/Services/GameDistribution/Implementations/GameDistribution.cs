using System;
using Newtonsoft.Json;
using UnityEngine;
#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using UnityEngine.Scripting;

namespace MassiveCore.Framework.Runtime
{
    public class GameDistribution : BaseMonoBehaviour, IGameDistribution
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SdkInitialize(string gameId, int debug);

        [DllImport("__Internal")]
        private static extern void SdkShowConsole();

        [DllImport("__Internal")]
        private static extern void SdkPreloadInterstitialAds();

        [DllImport("__Internal")]
        private static extern void SdkPreloadRewardedAds();

        [DllImport("__Internal")]
        private static extern void SdkShowBannerAds(string containerId);

        [DllImport("__Internal")]
        private static extern void SdkHideBannerAds(string containerId);

        [DllImport("__Internal")]
        private static extern void SdkShowInterstitialAds();

        [DllImport("__Internal")]
        private static extern void SdkShowRewardedAds();
#endif

        [SerializeField]
        private string _gameId;

        public event Action<bool> Initialized;
        public event Action Resumed;
        public event Action Paused;
        public event Action<string> AdsBannerShown;
        public event Action<string> AdsBannerHid;
        public event Action<string, string> AdsBannerError;
        public event Action AdsVideoLoaded;
        public event Action AdsVideoOpened;
        public event Action AdsVideoSkipped;
        public event Action AdsVideoComplete;
        public event Action<bool> AdsInterstitialPreloaded;
        public event Action<bool> AdsRewardedPreloaded;
        public event Action AdsInterstitialFinished;
        public event Action AdsRewardedFinished;

#if UNITY_WEBGL && !UNITY_EDITOR
        public void Initialize()
        {
#if DEBUG
            SdkInitialize(_gameId, 1);
#else
            SdkInitialize(_gameId, 0);
#endif
        }

        public void ShowConsole()
        {
            SdkShowConsole();
        }

        public void PreloadInterstitialAds()
        {
            SdkPreloadInterstitialAds();
        }

        public void PreloadRewardedAds()
        {
            SdkPreloadRewardedAds();
        }

        public void ShowBannerAds(string containerId)
        {
            SdkShowBannerAds(containerId);
        }

        public void HideBannerAds(string containerId)
        {
            SdkHideBannerAds(containerId);
        }

        public void ShowInterstitialAds()
        {
            SdkShowInterstitialAds();
        }

        public void ShowRewardedAds()
        {
            SdkShowRewardedAds();
        }
#else
        public void Initialize()
        {
            Initialized?.Invoke(true);
        }

        public void ShowConsole()
        {
            _logger.Print("Game Distribution show console!");
        }

        public void PreloadInterstitialAds()
        {
            AdsInterstitialPreloaded?.Invoke(true);
        }

        public void PreloadRewardedAds()
        {
            AdsRewardedPreloaded?.Invoke(true);
        }

        public void ShowBannerAds(string containerId)
        {
            AdsBannerShown?.Invoke(containerId);
        }

        public void HideBannerAds(string containerId)
        {
            AdsBannerHid?.Invoke(containerId);
        }

        public void ShowInterstitialAds()
        {
            AdsVideoLoaded?.Invoke();
            AdsVideoOpened?.Invoke();
            AdsVideoComplete?.Invoke();
            AdsInterstitialFinished?.Invoke();
        }

        public void ShowRewardedAds()
        {
            AdsVideoLoaded?.Invoke();
            AdsVideoOpened?.Invoke();
            AdsVideoComplete?.Invoke();
            AdsRewardedFinished?.Invoke();
        }
#endif

        [Preserve]
        public void OnInitialized(int result)
        {
            var success = result == 1;
            _logger.Print(success ? "Game Distribution: Initialized!" : "Game Distribution: Didn't initialize!");
            Initialized?.Invoke(success);
        }

        [Preserve]
        public void OnResumed()
        {
            _logger.Print("Game Distribution: Resumed!");
            Resumed?.Invoke();
        }

        [Preserve]
        public void OnPaused()
        {
            _logger.Print("Game Distribution: Paused!");
            Paused?.Invoke();
        }

        [Preserve]
        public void OnAdsBannerShown(string containerId)
        {
            _logger.Print($"Game Distribution: Ads banner \"{containerId}\" shown!");
            AdsBannerShown?.Invoke(containerId);
        }

        [Preserve]
        public void OnAdsBannerHid(string containerId)
        {
            _logger.Print($"Game Distribution: Ads banner \"{containerId}\" hid!");
            AdsBannerHid?.Invoke(containerId);
        }

        [Preserve]
        public void OnAdsBannerError(string json)
        {
            var arguments = JsonConvert.DeserializeObject<object[]>(json);
            var containerId = arguments[0] as string;
            var error = arguments[1] as string;
            _logger.Print($"Game Distribution: Ads banner \"{containerId}\" error \"{error})\"!");
            AdsBannerError?.Invoke(containerId, error);
        }

        [Preserve]
        public void OnAdsInterstitialPreloaded(int result)
        {
            var success = result == 1; 
            _logger.Print($"Game Distribution: Ads interstitial preloaded is {success}!");
            AdsInterstitialPreloaded?.Invoke(success);
        }

        [Preserve]
        public void OnAdsRewardedPreloaded(int result)
        {
            var success = result == 1; 
            _logger.Print($"Game Distribution: Ads rewarded preloaded is {success}!");
            AdsRewardedPreloaded?.Invoke(success);
        }

        [Preserve]
        public void OnAdsVideoLoaded()
        {
            _logger.Print("Game Distribution: Ads video loaded!");
            AdsVideoLoaded?.Invoke();
        }

        [Preserve]
        public void OnAdsVideoOpened()
        {
            _logger.Print("Game Distribution: Ads video opened!");
            AdsVideoOpened?.Invoke();
        }

        [Preserve]
        public void OnAdsVideoSkipped()
        {
            _logger.Print("Game Distribution: Ads video skipped!");
            AdsVideoSkipped?.Invoke();
        }

        [Preserve]
        public void OnAdsVideoComplete()
        {
            _logger.Print("Game Distribution: Ads video complete!");
            AdsVideoComplete?.Invoke();
        }

        [Preserve]
        public void OnAdsInterstitialFinished()
        {
            _logger.Print("Game Distribution: Ads interstitial finished!");
            AdsInterstitialFinished?.Invoke();
        }

        [Preserve]
        public void OnAdsRewardedFinished()
        {
            _logger.Print("Game Distribution: Ads rewarded finished!");
            AdsRewardedFinished?.Invoke();
        }
    }
}
