using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MassiveCore.Framework.Runtime.Patterns;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ExampleState : IState<UniTask>
    {
        [Inject]
        private readonly IAnalytics _analytics;

        [Inject]
        private readonly ITimers _timers;

        [Inject]
        private readonly IRemoteParameters _remoteParameters;

        [Inject]
        private readonly IEnvironment _environment;

        [Inject]
        private readonly ILevels _levels;

        [Inject]
        private readonly IScreens _screens;

        public async UniTask Enter(IStateArguments arguments)
        {
            _environment.ApplyConfig("example");
            _levels.LoadCurrentLevel();
            _screens.OpenScreen<ExampleScreen>();
        }

        public async UniTask Exit()
        {
        }

        private void TestAnalytics()
        {
            var dictionary = new Dictionary<string, object>
            {
                { "integer", 123 },
                { "float", 456.78f },
                { "bool", true }
            };
            _analytics.LogEvent("test", dictionary);
        }

        private void TestScreens()
        {
            _screens.OpenScreen<ExampleScreen>();   
        }

        private void TestTimers()
        {
            var arguments0 = new TimerArguments(TimeSpan.FromSeconds(15));
            var arguments1 = new TimerArguments(TimeSpan.MaxValue);
            _timers.Start<Timer>("000", arguments0);
            _timers.Start<Timer>("111", arguments1);
        }

        private void TestRemoteParameters()
        {
            var numberValue = _remoteParameters[RemoteParameterIds.Number].As<int>();
            var stringValue = _remoteParameters[RemoteParameterIds.String];
        }
    }
}
