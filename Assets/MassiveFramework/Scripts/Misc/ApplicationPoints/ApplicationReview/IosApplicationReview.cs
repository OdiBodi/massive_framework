#if UNITY_IOS

using System.Threading.Tasks;
using UnityEngine.iOS;
using Zenject;

namespace Squares
{
    public class IosApplicationReview : IApplicationReview
    {
        [Inject]
        private readonly IProfile profile;
        
        public async Task<bool> Request()
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
