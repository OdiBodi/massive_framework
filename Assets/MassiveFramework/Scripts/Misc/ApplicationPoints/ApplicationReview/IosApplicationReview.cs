#if UNITY_IOS

using Cysharp.Threading.Tasks;
using UnityEngine.iOS;
using Zenject;

namespace Squares
{
    public class IosApplicationReview : IApplicationReview
    {
        [Inject]
        private readonly ICustomProfile profile;
        
        public async UniTask<bool> Request()
        {
            if (profile.ApplicationReviewActive.Value)
            {
                Device.RequestStoreReview();
                profile.ApplicationReviewActive.Value = false;
                return true; 
            }
            return false;
        }
    }
}

#endif // UNITY_IOS
