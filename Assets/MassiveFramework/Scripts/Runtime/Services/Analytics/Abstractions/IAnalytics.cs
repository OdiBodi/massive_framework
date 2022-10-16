using System.Collections.Generic;

namespace MassiveCore.Framework
{
    public interface IAnalytics
    {
        void Initialize();
        void LogEvent(string name, IDictionary<string, object> parameters);
    }
}
