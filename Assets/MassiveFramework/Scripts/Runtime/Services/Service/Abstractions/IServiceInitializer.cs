using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public interface IServiceInitializer
    {
        ReadOnlyReactiveProperty<bool> Initialized { get; }
        UniTask<bool> Initialize();
    }
}
