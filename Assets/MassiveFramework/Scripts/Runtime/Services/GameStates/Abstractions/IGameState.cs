using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IGameState
    {
        public UniTask Enter(IGameState previous);
        public UniTask Exit();
    }
}
