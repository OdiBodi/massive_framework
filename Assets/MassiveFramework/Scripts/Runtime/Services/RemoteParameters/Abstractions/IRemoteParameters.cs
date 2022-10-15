using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IRemoteParameters
    {
        string this[string name] { get; }
        UniTask<bool> Fetch();
        IRemoteParameter Parameter(string name);
    }
}
