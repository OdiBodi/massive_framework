using Zenject;

namespace MassiveCore.Framework
{
    public class GamePoint : ApplicationPoint
    {
        [Inject]
        private readonly Game game;

        public override void Init()
        {
            Complete();
            game.StartGame();
        }
    }
}
