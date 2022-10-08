using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework
{
    public interface IProfile
    {
        event Action OnPreLoading;
        event Action OnPostLoading;
        event Action OnPreSaving;
        event Action OnPostSaving;
        ReactiveProperty<T> Property<T>(string id, T defaultValue = default);
        ReactiveCollection<T> Collection<T>(string id, T[] defaultValue = default);
        UniTask<bool> Synchronize();
    }
}
