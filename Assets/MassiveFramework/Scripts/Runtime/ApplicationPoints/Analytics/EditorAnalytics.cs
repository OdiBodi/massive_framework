using Zenject;

namespace MassiveCore.Framework
{
    public class EditorAnalytics : IAnalytics
    {
        [Inject]
        private readonly ILogger logger;

        public void Init()
        {
        }
/*
        public void Example(int first, string second, bool third)
        {
            logger.Print
            (
                "Analytics.StepFailed():" +
                $"\nfirst={first}" +
                $"\nsecond={second}" +
                $"\nthird={third}"
            );
        }
*/
    }
}
