using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public static class ScreenExtensions
    {
        public static async UniTask CloseOnNextFrame(this Screen screen, ScreenClosingResult result)
        {
            await Observable.NextFrame();
            screen.Close(result);
        }
    }
}
