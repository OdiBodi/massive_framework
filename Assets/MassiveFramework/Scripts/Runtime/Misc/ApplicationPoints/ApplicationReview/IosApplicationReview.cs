#if UNITY_IOS

using Cysharp.Threading.Tasks;
using UnityEngine.iOS;
using Zenject;

namespace MassiveCore.Framework
{
    public class IosApplicationReview : IApplicationReview
    {
        [Inject]
        private readonly ICustomProfile profile;

        public async UniTask<bool> Request()
        {
            if (!profile.ApplicationReviewActive.Value)
            {
                return true;
            }
            Device.RequestStoreReview();
            profile.ApplicationReviewActive.Value = false;
            return true; 
        }
    }
}

#endif // UNITY_IOS
