using System.Collections.Generic;

namespace MassiveCore.Framework.Runtime
{
    public interface IScreens
    {
        IEnumerable<Screen> ScreenInstances { get; }
        IEnumerable<Screen> TopScreenInstances { get; }

        T OpenScreen<T>() where T : Screen;
        T OpenTopScreen<T>() where T : Screen;

        void CloseScreens();
        void CloseTopScreens();

        T ScreenInstance<T>() where T : Screen;
        T TopScreenInstance<T>() where T : Screen;
    }
}
