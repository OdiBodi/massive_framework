#if UNITY_IOS || UNITY_ANDROID

using System.Collections.Generic;

namespace MassiveCore.Framework.Runtime
{
    public class Analytics : IAnalytics
    {
        public void Initialize()
        {
        }

        public void LogEvent(string name)
        {
            AppMetrica.Instance.ReportEvent(name);
        }

        public void LogEvent(string name, IDictionary<string, object> parameters)
        {
            AppMetrica.Instance.ReportEvent(name, parameters);
        }
    }
}

#endif
