using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public interface ILevel
    {
        ReadOnlyReactiveProperty<bool> Loaded { get; }
    }
}
