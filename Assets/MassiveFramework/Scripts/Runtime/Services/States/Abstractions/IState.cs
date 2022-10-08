using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IState
    {
        public UniTask Enter(IState previous);
        public UniTask Exit();
    }
}
