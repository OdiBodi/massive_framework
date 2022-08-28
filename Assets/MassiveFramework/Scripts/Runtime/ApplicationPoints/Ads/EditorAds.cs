using System;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorAds : IAds
    {
        [Inject]
        private readonly ILogger logger;
        
        public bool CanShowInterstitial => true;
        public bool CanShowRewarded => true;
        public bool HasBanner => true;
        public bool HasInterstitial => true;
        public bool HasRewarded => true;

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
            logger.Print("AdEditor.Init()");
            OnInitialized?.Invoke();
        }

        public void ShowBanner()
        {
            logger.Print("AdEditor.ShowBanner()");
            OnBannerLoaded?.Invoke(true);
            OnBannerShown?.Invoke();
        }

        public void ShowInterstitial()
        {
            logger.Print("AdEditor.ShowInterstitial()");
            OnInterstitialLoaded?.Invoke(true);
            OnInterstitialOpened?.Invoke(true);
            OnInterstitialClosed?.Invoke();
        }

        public bool ShowRewarded(string tag)
        {
            logger.Print("AdEditor.ShowRewarded()");
            OnRewardedLoaded?.Invoke(true, tag);
            OnRewardedOpened?.Invoke(true, tag);
            OnRewardedClosed?.Invoke(true, tag);
            return true;
        }
    }
}
