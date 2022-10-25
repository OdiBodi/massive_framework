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

        private ReactiveProperty<bool> ApplicationReviewActiveProperty => _profile.Property<bool>(ProfileIds.ApplicationReviewActive);

        public async UniTask<bool> Request()
        {
            if (!ApplicationReviewActiveProperty.Value)
            {
                return true;
            }
            Device.RequestStoreReview();
            ApplicationReviewActiveProperty.Value = false;
            return true; 
        }
    }
}

#endif // UNITY_IOS
