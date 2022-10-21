using UniRx;

namespace MassiveCore.Framework
{
    public interface ILevel
    {
        ReadOnlyReactiveProperty<bool> Loaded { get; }
    }
}
