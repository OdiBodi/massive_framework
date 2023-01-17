using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public interface IScreenResolution
    {
        ReadOnlyReactiveProperty<Resolution> Resolution { get; }
    }
}
