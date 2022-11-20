using UniRx;

namespace MassiveCore.Framework
{
    public interface IScreenResolution
    {
        ReadOnlyReactiveProperty<Resolution> Resolution { get; }
    }
}
