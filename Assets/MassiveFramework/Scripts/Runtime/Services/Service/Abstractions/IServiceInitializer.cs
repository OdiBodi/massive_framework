using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework
{
    public interface IServiceInitializer
    {
        public ReadOnlyReactiveProperty<bool> Initialized { get; }
        UniTask<bool> Initialize();
    }
}
