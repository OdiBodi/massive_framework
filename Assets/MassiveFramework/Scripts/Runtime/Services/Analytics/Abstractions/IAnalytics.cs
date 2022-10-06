using System.Collections.Generic;

namespace MassiveCore.Framework
{
    public interface IAnalytics
    {
        void Init();
        void LogEvent(string name, IDictionary<string, object> parameters);
    }
}
