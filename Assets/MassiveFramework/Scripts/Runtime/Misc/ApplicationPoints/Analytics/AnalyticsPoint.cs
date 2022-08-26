using Zenject;

namespace MassiveCore.Framework
{
    public class AnalyticsPoint : ApplicationPoint
    {
        [Inject]
        private readonly IAnalytics analytics;

        public override void Init()
        {
            analytics.Init();
            Complete();
        }
    }
}
