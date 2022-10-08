using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework
{
    public interface IResource
    {
        string Id { get; }
        ReadOnlyReactiveProperty<int> Amount { get; }
        IEnumerable<T> Configs<T>() where T : ResourceConfig;
        UniTask<bool> Spend(int amount);
        UniTask<bool> Increase(int amount);
    }
}
