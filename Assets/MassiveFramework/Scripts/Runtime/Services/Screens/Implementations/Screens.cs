using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
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
        private IEnumerable<Screen> ScreenInstances => ScreenAllInstances.Where(Screen);
        private IEnumerable<Screen> TopScreenInstances => ScreenAllInstances.Where(TopScreen);
        private int LastScreenOrder => ScreenAllInstances.Count(Screen);
        private int LastTopScreenOrder => _originTopOrder + ScreenAllInstances.Count(TopScreen);

        public UniTask<ScreenClosingResult> ShowScreen<T>(Action<T> onCreated = null)
            where T : Screen
        {
            return ShowScreen(LastScreenOrder, onCreated);
        }

        public UniTask<ScreenClosingResult> ShowTopScreen<T>(Action<T> onCreated = null)
            where T : Screen
        {
            return ShowScreen(LastTopScreenOrder, onCreated);
        }

        public void CloseScreens()
        {
            ScreenInstances.ForEach(x => x.Close(ScreenClosingResult.Close));
        }

        public void CloseTopScreens()
        {
            TopScreenInstances.ForEach(x => x.Close(ScreenClosingResult.Close));
        }

        private async UniTask<ScreenClosingResult> ShowScreen<T>(int order, Action<T> onCreated = null)
            where T : Screen
        {
            var screen = _screenFactory.Create(typeof(T), _root) as T;
            screen.Order = order;
            onCreated?.Invoke(screen);
            var result = await screen.WaitForClose();
            UpdateScreenOrdersOnNextFrame();
            return result;
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
