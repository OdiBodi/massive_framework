using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class AnalyticsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IAnalytics _analytics;

        public override UniTask<bool> Initialize()
        {
            _analytics.Init();
            CompleteInitialize(true);
            return base.Initialize();
        }
    }
}
