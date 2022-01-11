using Zenject;

namespace MassiveCore.Framework
{
    public class Game
    {
        [Inject]
        private readonly Levels levels;

        [Inject]
        private readonly Screens screens;

        public void StartGame()
        {
            levels.LoadCurrentLevel();
            screens.ShowBottomScreen<ExampleScreen>();
        }
    }
}
