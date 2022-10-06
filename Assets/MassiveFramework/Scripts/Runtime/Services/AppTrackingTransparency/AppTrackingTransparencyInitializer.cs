using Cysharp.Threading.Tasks;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace MassiveCore.Framework
{
    public class AppTrackingTransparencyInitializer : ServiceInitializer
    {
        public override UniTask<bool> Initialize()
        {
#if UNITY_IOS && !UNITY_EDITOR
            var result = ATTrackingStatusBinding.GetAuthorizationTrackingStatus(); 
            if (result == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking(status => CompleteInitialize(true));
            }
#else
            CompleteInitialize(true);
#endif
            return base.Initialize();
        }
    }
}
