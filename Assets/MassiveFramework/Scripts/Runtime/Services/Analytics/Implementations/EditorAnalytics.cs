using System.Collections.Generic;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorAnalytics : IAnalytics
    {
        [Inject]
        private readonly ILogger _logger;

        public void Init()
        {
        }

        public void LogEvent(string name, IDictionary<string, object> parameters)
        {
            var formattedParameters = parameters.ToFormattedString();
            var text = $"Analytics event \"{name}\":\n{formattedParameters}";
            _logger.Print(text);
        }
    }
}
