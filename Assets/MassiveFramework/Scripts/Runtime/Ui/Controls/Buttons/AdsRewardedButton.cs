using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class AdsRewardedButton : BaseMonoBehaviour
    {
        [Inject]
        private readonly IAds _ads;

        [SerializeField]
        private BaseButton _button;

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
            _ads.RewardedLoaded += OnRewardedLoaded;
            _ads.RewardedClosed += OnRewardedClosed;
        }

        private void UnsubscribeFromAds()
        {
            _ads.RewardedLoaded -= OnRewardedLoaded;
            _ads.RewardedClosed -= OnRewardedClosed;
        }

        private void UpdateView()
        {
            _button.Enabled = _ads.RewardedReady;
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
