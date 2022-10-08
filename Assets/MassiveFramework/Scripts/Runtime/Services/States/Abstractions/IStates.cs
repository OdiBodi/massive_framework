using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IStates
    {
        IState CurrentState { get; }
        T State<T>() where T : class, IState;
        void BindState<T>(T state) where T : class, IState;
        UniTask GoTo<T>() where T : class, IState;
    }
}
