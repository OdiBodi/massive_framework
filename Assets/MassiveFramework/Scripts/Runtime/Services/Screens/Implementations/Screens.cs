using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class Screens : IScreens
    {
        [Inject]
        private readonly ScreenFactory _screenFactory;

        private readonly Transform _root;
        private readonly int _originTopOrder;

        public Screens(Transform root, int originTopOrder)
        {
            _root = root;
            _originTopOrder = originTopOrder;
        }

        private IEnumerable<Screen> ScreenAllInstances => _root.gameObject.Descendants().OfComponent<Screen>();
        public IEnumerable<Screen> ScreenInstances => ScreenAllInstances.Where(Screen);
        public IEnumerable<Screen> TopScreenInstances => ScreenAllInstances.Where(TopScreen);
        private int LastScreenOrder => ScreenAllInstances.Count(Screen);
        private int LastTopScreenOrder => _originTopOrder + ScreenAllInstances.Count(TopScreen);

        public T OpenScreen<T>()
            where T : Screen
        {
            return OpenScreen<T>(LastScreenOrder);
        }

        public T OpenTopScreen<T>()
            where T : Screen
        {
            return OpenScreen<T>(LastTopScreenOrder);
        }

        public void CloseScreens()
        {
            ScreenInstances.ForEach(x => x.Close(ScreenClosingResult.Close));
        }

        public void CloseTopScreens()
        {
            TopScreenInstances.ForEach(x => x.Close(ScreenClosingResult.Close));
        }

        public T ScreenInstance<T>()
            where T : Screen
        {
            var screen = ScreenInstances.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
            return screen;
        }

        public T TopScreenInstance<T>()
            where T : Screen
        {
            var screen = TopScreenInstances.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
            return screen;
        }

        private T OpenScreen<T>(int order)
            where T : Screen
        {
            var screen = _screenFactory.Create(typeof(T), _root) as T;
            screen.Order = order;
            UpdateScreenOrdersOnNextFrame();
            return screen;
        }

        private async UniTask UpdateScreenOrdersOnNextFrame()
        {
            await Observable.NextFrame();
            ScreenInstances.UpdateOrders(0);
            TopScreenInstances.UpdateOrders(_originTopOrder);
        }

        private bool Screen(Screen screen)
        {
            return screen.Order < _originTopOrder;
        }

        private bool TopScreen(Screen screen)
        {
            return screen.Order >= _originTopOrder;
        }
    }
}
