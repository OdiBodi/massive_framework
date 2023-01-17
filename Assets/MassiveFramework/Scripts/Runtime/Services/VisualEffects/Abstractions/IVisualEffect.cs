using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime
{
    public interface IVisualEffect
    {
        string Id { get; }
        UniTask Play();
        void Stop();
    }
}
