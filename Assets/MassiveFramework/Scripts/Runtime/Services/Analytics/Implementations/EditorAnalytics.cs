using System.Collections.Generic;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorAnalytics : IAnalytics
    {
        [Inject]
        private readonly ILogger _logger;

        public void Initialize()
        {
        }

        public void LogEvent(string name)
        {
            var text = $"Analytics event \"{name}\"";
            _logger.Print(text);
        }

        public void LogEvent(string name, IDictionary<string, object> parameters)
        {
            var formattedParameters = parameters.ToFormattedString();
            var text = $"Analytics event \"{name}\":\n{formattedParameters}";
            _logger.Print(text);
        }
    }
}
