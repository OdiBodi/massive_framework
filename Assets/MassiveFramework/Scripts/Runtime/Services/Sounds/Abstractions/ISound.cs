using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime
{
    public interface ISound
    {
        string Id { get; }
        bool Playing { get; }
        UniTask Play(float volumeScale = 1f, float pitchScale = 1f);
        void Stop();
    }
}
