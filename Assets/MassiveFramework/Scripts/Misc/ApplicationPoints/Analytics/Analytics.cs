using System.Collections.Generic;

namespace MassiveCore.Framework
{
    public class Analytics : IAnalytics
    {
        public void Init()
        {
        }
/*
        public void Example(int first, string second, bool third)
        {
            var parameters = new Dictionary<string, object>
            {
                { "first", first },
                { "second", second },
                { "third", third },
            };
            LogEvent("step_failed", parameters);
        }
*/
        private static void LogEvent(string name, Dictionary<string, object> parameters)
        {
            AppMetrica.Instance.ReportEvent(name, parameters);
        }
    }
}
