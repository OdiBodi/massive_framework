using Zenject;

namespace MassiveCore.Framework
{
    public class AdsPoint : ApplicationPoint
    {
        [Inject]
        private readonly IAds ads;

        public override void Init()
        {
            SubscribeOnAds();
            ads.Init();
        }

        private void SubscribeOnAds()
        {
            ads.OnInitialized += Complete;
        }
    }
}
