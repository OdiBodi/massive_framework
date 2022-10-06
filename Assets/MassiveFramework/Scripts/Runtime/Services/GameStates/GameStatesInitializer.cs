using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class GameStatesInitializer : ServiceInitializer
    {
        [Inject]
        private readonly GameStateFactory _gameStateFactory;

        [Inject]
        private readonly IGameStates _gameStates;

        public override UniTask<bool> Initialize()
        {
            BindGameStates();

            _gameStates.GoTo<ExampleGameState>();

            CompleteInitialize(true);
            return base.Initialize();
        }

        private void BindGameStates()
        {
            var exampleGameState = _gameStateFactory.Create<ExampleGameState>();
            _gameStates.BindState(exampleGameState);
        }
    }
}
