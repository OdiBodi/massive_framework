using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IScreens
    {
        UniTask<ScreenClosingResult> ShowScreen<T>(Action<T> onCreated = null) where T : Screen;
        UniTask<ScreenClosingResult> ShowTopScreen<T>(Action<T> onCreated = null) where T : Screen;
        void CloseScreens();
        void CloseTopScreens();
    }
}
