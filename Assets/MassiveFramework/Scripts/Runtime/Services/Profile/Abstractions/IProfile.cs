using UniRx;

namespace MassiveCore.Framework
{
    public interface IProfile
    {
        ReactiveProperty<T> Property<T>(string id, T defaultValue = default);
        ReactiveCollection<T> Collection<T>(string id, T[] defaultValue = default);
        void Sync();
    }
}
