using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface ISound
    {
        string Id { get; }
        UniTask Play(float volumeScale = 1f, float pitchScale = 1f);
        void Stop();
    }
}
