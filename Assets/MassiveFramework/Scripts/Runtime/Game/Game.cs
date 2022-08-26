using Zenject;

namespace MassiveCore.Framework
{
    public class Game
    {
        [Inject]
        private readonly Environment environment;

        [Inject]
        private readonly Levels levels;

        [Inject]
        private readonly Screens screens;

        public async void StartGame()
        {
            environment.ApplyConfig("example");
            levels.LoadCurrentLevel();
            var closed = await screens.ShowBottomScreen<ExampleScreen>() == ScreenClosingResult.Close;
        }
    }
}
