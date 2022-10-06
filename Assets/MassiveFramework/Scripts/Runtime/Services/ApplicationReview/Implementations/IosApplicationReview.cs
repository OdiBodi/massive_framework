#if UNITY_IOS

using Cysharp.Threading.Tasks;
using UnityEngine.iOS;
using Zenject;

namespace MassiveCore.Framework
{
    public class IosApplicationReview : IApplicationReview
    {
        [Inject]
        private readonly IProfile _profile;

        private ReactiveProperty<bool> ApplicationReviewActive => _profile.Property<bool>(ProfileIds.ApplicationReviewActive);

        public async UniTask<bool> Request()
        {
            if (!ApplicationReviewActive.Value)
            {
                return true;
            }
            Device.RequestStoreReview();
            ApplicationReviewActive.Value = false;
            return true; 
        }
    }
}

#endif // UNITY_IOS
