using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IGameStates
    {
        IGameState CurrentState { get; }
        T State<T>() where T : class, IGameState;
        void BindState<T>(T gameState) where T : class, IGameState;
        UniTask GoTo<T>() where T : class, IGameState;
    }
}
