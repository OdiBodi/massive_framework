using System.Collections.Generic;

namespace MassiveCore.Framework.Runtime
{
    public interface IAnalytics
    {
        void Initialize();
        void LogEvent(string name);
        void LogEvent(string name, IDictionary<string, object> parameters);
    }
}
