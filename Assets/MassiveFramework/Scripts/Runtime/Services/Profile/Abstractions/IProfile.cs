using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public interface IProfile
    {
        event Action PreLoading;
        event Action PostLoading;
        event Action PreSaving;
        event Action PostSaving;
        ReactiveProperty<T> Property<T>(string id, T defaultValue = default);
        ReactiveCollection<T> Collection<T>(string id, T[] defaultValue = default);
        UniTask<bool> Synchronize();
    }
}
