using System;
#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif
using UnityEngine.Scripting;

namespace MassiveCore.Framework
{
    public class Yandex : BaseMonoBehaviour, IYandex
    {
#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void ShowBannerAdv();

        [DllImport("__Internal")]
        private static extern void HideBannerAdv();

        [DllImport("__Internal")]
        private static extern void ShowFullscreenAdv();

        [DllImport("__Internal")]
        private static extern void ShowRewardedVideo();
#endif

        public event Action InterstitialAdsOpened;
        public event Action<bool> InterstitialAdsClosed;
        public event Action<string> InterstitialAdsError;

        public event Action RewardedAdsOpened;
        public event Action RewardedAdsRewarded;
        public event Action RewardedAdsClosed;
        public event Action<string> RewardedAdsError;

#if UNITY_WEBGL
        public void ShowBannerAds()
        {
            ShowBannerAdv();
        }

        public void HideBannerAds()
        {
            HideBannerAdv();
        }

        public void ShowInterstitialAds()
        {
            ShowFullscreenAdv();
        }

        public void ShowRewardedAds()
        {
            ShowRewardedVideo();
        }
#else
        public void ShowBannerAds()
        {
            _logger.Print("Yandex Banner Ads show!");
        }

        public void HideBannerAds()
        {
            _logger.Print("Yandex Banner Ads hide!");
        }

        public void ShowInterstitialAds()
        {
            OnInterstitialAdsOpened();
            OnInterstitialAdsClosed(1);
        }

        public void ShowRewardedAds()
        {
            OnRewardedAdsOpened();
            OnRewardedAdsRewarded();
            OnRewardedAdsClosed();
        }
#endif

        [Preserve]
        public void OnInterstitialAdsOpened()
        {
            _logger.Print("Yandex Interstitial Ads opened!");
            InterstitialAdsOpened?.Invoke();
        }

        [Preserve]
        public void OnInterstitialAdsClosed(int wasShown)
        {
            var result = wasShown != 0; 
            _logger.Print($"Yandex Interstitial Ads closed: wasShown={result}!");
            InterstitialAdsClosed?.Invoke(result);
        }

        [Preserve]
        public void OnInterstitialAdsError(string error)
        {
            _logger.PrintError($"Yandex Interstitial Ads error: {error}!");
            InterstitialAdsError?.Invoke(error);
        }

        [Preserve]
        public void OnRewardedAdsOpened()
        {
            _logger.Print("Yandex Rewarded Ads opened!");
            RewardedAdsOpened?.Invoke();
        }

        [Preserve]
        public void OnRewardedAdsRewarded()
        {
            _logger.Print("Yandex Rewarded Ads rewarded!");
            RewardedAdsRewarded?.Invoke();
        }

        [Preserve]
        public void OnRewardedAdsClosed()
        {
            _logger.Print("Yandex Rewarded Ads closed!");
            RewardedAdsClosed?.Invoke();
        }

        [Preserve]
        public void OnRewardedAdsError(string error)
        {
            _logger.PrintError($"Yandex Rewarded Ads error: {error}!");
            RewardedAdsError?.Invoke(error);
        }
    }
}
