#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace MassiveCore.Framework
{
    public class AppTrackingTransparencyPoint : ApplicationPoint
    {
        public override void Init()
        {
#if UNITY_IOS && !UNITY_EDITOR
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking(status => Complete());
                return;
            }
#endif
            Complete();
        }
    }
}
