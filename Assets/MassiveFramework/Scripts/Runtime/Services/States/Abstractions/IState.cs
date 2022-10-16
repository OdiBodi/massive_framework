using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IState
    {
        UniTask Enter(IState previous);
        UniTask Exit();
    }
}
