using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IVisualEffect
    {
        string Id { get; }
        UniTask Play();
        void Stop();
    }
}
