using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class AnalyticsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IAnalytics _analytics;

        public override UniTask<bool> Initialize()
        {
            _analytics.Initialize();
            CompleteInitialize(true);
            return base.Initialize();
        }
    }
}
