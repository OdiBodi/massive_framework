using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class AdsRewardedButton : BaseMonoBehaviour
    {
        [Inject]
        private readonly IAds ads;

        [SerializeField]
        private BaseButton button;

        private void Awake()
        {
            SubscribeOnAds();
            UpdateView();
        }

        private void OnDestroy()
        {
            UnsubscribeFromAds();
        }

        private void SubscribeOnAds()
        {
            ads.OnRewardedLoaded += OnRewardedLoaded;
            ads.OnRewardedClosed += OnRewardedClosed;
        }

        private void UnsubscribeFromAds()
        {
            ads.OnRewardedLoaded -= OnRewardedLoaded;
            ads.OnRewardedClosed -= OnRewardedClosed;
        }

        private void UpdateView()
        {
            button.Enabled = ads.HasRewarded;
        }

        private void OnRewardedLoaded(bool loaded, string tag)
        {
            UpdateView();
        }

        private void OnRewardedClosed(bool result, string tag)
        {
            UpdateView();
        }
    }
}
