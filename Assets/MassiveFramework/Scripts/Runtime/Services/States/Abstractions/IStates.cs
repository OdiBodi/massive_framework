using Cysharp.Threading.Tasks;
using MassiveCore.Framework.Runtime.Patterns;

namespace MassiveCore.Framework.Runtime
{
    public interface IStates : IStateContext<UniTask>
    {
        T State<T>() where T : class, IState<UniTask>;
        void BindState<T>(T state) where T : class, IState<UniTask>;
    }
}
