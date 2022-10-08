using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class ExampleState : IState
    {
        [Inject]
        private readonly IAnalytics _analytics;

        [Inject]
        private readonly ITimers _timers;

        [Inject]
        private readonly IEnvironment _environment;

        [Inject]
        private readonly ILevels _levels;

        [Inject]
        private readonly IScreens _screens;

        public async UniTask Enter(IState previous)
        {
            _environment.ApplyConfig("example");
            _levels.LoadCurrentLevel();
            _screens.ShowScreen<ExampleScreen>();
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
            _screens.ShowScreen<ExampleScreen>();   
        }

        private void TestTimers()
        {
            _timers.Start<Timer>("000", TimeSpan.FromSeconds(15));
            _timers.Start<Timer>("111", TimeSpan.MaxValue);
        }
    }
}
