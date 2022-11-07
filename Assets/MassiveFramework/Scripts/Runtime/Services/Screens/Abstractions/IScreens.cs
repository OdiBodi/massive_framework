using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IScreens
    {
        IEnumerable<Screen> ScreenInstances { get; }
        IEnumerable<Screen> TopScreenInstances { get; }

        UniTask<ScreenClosingResult> ShowScreen<T>(Action<T> onCreated = null) where T : Screen;
        UniTask<ScreenClosingResult> ShowTopScreen<T>(Action<T> onCreated = null) where T : Screen;

        void CloseScreens();
        void CloseTopScreens();

        T ScreenInstance<T>() where T : Screen;
        T TopScreenInstance<T>() where T : Screen;
    }
}
