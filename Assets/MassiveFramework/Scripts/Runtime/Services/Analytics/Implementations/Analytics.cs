using System.Collections.Generic;

namespace MassiveCore.Framework
{
    public class Analytics : IAnalytics
    {
        public void Initialize()
        {
        }

        public void LogEvent(string name, IDictionary<string, object> parameters)
        {
            AppMetrica.Instance.ReportEvent(name, parameters);
        }
    }
}
