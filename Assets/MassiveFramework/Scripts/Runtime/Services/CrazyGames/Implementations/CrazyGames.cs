using System;
using Newtonsoft.Json;
#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif
using UnityEngine.Scripting;

namespace MassiveCore.Framework.Runtime
{
    public class CrazyGames : BaseMonoBehaviour, ICrazyGames
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SdkGameLoadingStart();

        [DllImport("__Internal")]
        private static extern void SdkGameLoadingStop();

        [DllImport("__Internal")]
        private static extern void SdkGameplayStart();

        [DllImport("__Internal")]
        private static extern void SdkGameplayStop();

        [DllImport("__Internal")]
        private static extern void SdkHappyTime();

        [DllImport("__Internal")]
        private static extern void SdkRequestBannerAds(string containerId, int width, int height);

        [DllImport("__Internal")]
        private static extern void SdkRequestResponsiveBannerAds(string containerId);
        
        [DllImport("__Internal")]
        private static extern void SdkHideBannerAds(string containerId);

        [DllImport("__Internal")]
        private static extern void SdkRequestInterstitialAds();

        [DllImport("__Internal")]
        private static extern void SdkRequestRewardedAds();
#endif

        public event Action<string> AdsBannerShown;
        public event Action<string> AdsBannerHid;
        public event Action<string, string> AdsBannerError;
        public event Action AdsInterstitialStarted;
        public event Action AdsInterstitialFinished;
        public event Action<string> AdsInterstitialError;
        public event Action AdsRewardedStarted;
        public event Action AdsRewardedFinished;
        public event Action<string> AdsRewardedError;

#if UNITY_WEBGL && !UNITY_EDITOR
        public void LoadingStart()
        {
            SdkGameLoadingStart();
        }

        public void LoadingStop()
        {
            SdkGameLoadingStop();
        }

        public void GameplayStart()
        {
            SdkGameplayStart();
        }

        public void GameplayStop()
        {
            SdkGameplayStop();
        }

        public void HappyTime()
        {
            SdkHappyTime();
        }

        public void RequestBannerAds(string containerId, int width, int height)
        {
            SdkRequestBannerAds(containerId, width, height);
        }

        public void RequestResponsiveBannerAds(string containerId)
        {
            SdkRequestResponsiveBannerAds(containerId);
        }

        public void HideBannerAds(string containerId)
        {
            SdkHideBannerAds(containerId);
        }

        public void RequestInterstitialAds()
        {
            SdkRequestInterstitialAds();
        }

        public void RequestRewardedAds()
        {
            SdkRequestRewardedAds();
        }
#else

        public void LoadingStart()
        {
            _logger.Print("Crazy Games: Loading start!");
        }

        public void LoadingStop()
        {
            _logger.Print("Crazy Games: Loading stop!");
        }

        public void GameplayStart()
        {
            _logger.Print("Crazy Games: Gameplay start!");
        }

        public void GameplayStop()
        {
            _logger.Print("Crazy Games: Gameplay stop!");
        }

        public void HappyTime()
        {
            _logger.Print("Crazy Games: Happy Time!");
        }

        public void RequestBannerAds(string containerId, int width, int height)
        {
            AdsBannerShown?.Invoke(containerId);
        }

        public void RequestResponsiveBannerAds(string containerId)
        {
            AdsBannerShown?.Invoke(containerId);
        }

        public void HideBannerAds(string containerId)
        {
            AdsBannerHid?.Invoke(containerId);
        }

        public void RequestInterstitialAds()
        {
            AdsInterstitialStarted?.Invoke();
            AdsInterstitialFinished?.Invoke();
        }

        public void RequestRewardedAds()
        {
            AdsRewardedStarted?.Invoke();
            AdsRewardedFinished?.Invoke();
        }
#endif

        [Preserve]
        private void OnAdsBannerShown(string containerId)
        {
            _logger.Print($"Crazy Games: Ads banner \"{containerId}\" shown!");
            AdsBannerShown?.Invoke(containerId);
        }

        [Preserve]
        private void OnAdsBannerHid(string containerId)
        {
            _logger.Print($"Crazy Games: Ads banner \"{containerId}\" hid!");
            AdsBannerHid?.Invoke(containerId);
        }

        [Preserve]
        private void OnAdsBannerError(string json)
        {
            var arguments = JsonConvert.DeserializeObject<object[]>(json);
            var containerId = arguments[0] as string;
            var error = arguments[1] as string;
            _logger.Print($"Crazy Games: Ads banner \"{containerId}\" error \"{error})\"!");
            AdsBannerError?.Invoke(containerId, error);
        }

        [Preserve]
        public void OnAdsInterstitialStarted()
        {
            _logger.Print("Crazy Games: Ads interstitial started!");
            AdsInterstitialStarted?.Invoke();
        }

        [Preserve]
        public void OnAdsInterstitialError(string error)
        {
            _logger.Print($"Crazy Games: Ads interstitial error \"{error}\"!");
            AdsInterstitialError?.Invoke(error);
        }

        [Preserve]
        public void OnAdsInterstitialFinished()
        {
            _logger.Print("Crazy Games: Ads interstitial finished!");
            AdsInterstitialFinished?.Invoke();
        }
        
        [Preserve]
        public void OnAdsRewardedStarted()
        {
            _logger.Print("Crazy Games: Ads rewarded started!");
            AdsRewardedStarted?.Invoke();
        }

        [Preserve]
        public void OnAdsRewardedError(string error)
        {
            _logger.Print($"Crazy Games: Ads rewarded error \"{error}\"!");
            AdsRewardedError?.Invoke(error);
        }

        [Preserve]
        public void OnAdsRewardedFinished()
        {
            _logger.Print("Crazy Games: Ads rewarded finished!");
            AdsRewardedFinished?.Invoke();
        }
    }
}
