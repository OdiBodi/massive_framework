using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniRx;
using Unity.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class Screens
    {
        [Inject]
        private readonly Screen.Factory screenFactory;

        private readonly Transform root;
        private readonly int originTopOrder;

        public Screens(Transform root, int originTopOrder)
        {
            this.root = root;
            this.originTopOrder = originTopOrder;
        }

        private IEnumerable<Screen> InstanceScreens => root.gameObject.Descendants().OfComponent<Screen>();
        private IEnumerable<Screen> InstanceBottomScreens => InstanceScreens.Where(BottomScreen);
        private IEnumerable<Screen> InstanceTopScreens => InstanceScreens.Where(TopScreen);
        private int LastBottomScreenOrder => InstanceScreens.Count(BottomScreen);
        private int LastTopScreenOrder => originTopOrder + InstanceScreens.Count(TopScreen);

        public Task<ScreenClosingResult> ShowBottomScreen<T>(Action<T> onCreated = null) where T : Screen
        {
            return ShowScreen(LastBottomScreenOrder, onCreated);
        }

        public Task<ScreenClosingResult> ShowTopScreen<T>(Action<T> onCreated = null) where T : Screen
        {
            return ShowScreen(LastTopScreenOrder, onCreated);
        }

        public void CloseBottomScreens()
        {
            InstanceBottomScreens.ForEach(x => x.Close(ScreenClosingResult.Close));
        }

        public void CloseTopScreens()
        {
            InstanceTopScreens.ForEach(x => x.Close(ScreenClosingResult.Close));
        }

        private async Task<ScreenClosingResult> ShowScreen<T>(int order, Action<T> onCreated = null) where T : Screen
        {
            var screen = screenFactory.Create(typeof(T)) as T;
            screen.CacheTransform.SetParent(root, false);
            screen.Order = order;
            onCreated?.Invoke(screen);
            var result = await screen.WaitForClose();
            UpdateScreenOrdersOnNextFrame();
            return result;
        }

        private async Task UpdateScreenOrdersOnNextFrame()
        {
            await Observable.NextFrame();
            InstanceBottomScreens.UpdateOrders(0);
            InstanceTopScreens.UpdateOrders(originTopOrder);
        }

        private bool BottomScreen(Screen screen)
        {
            return screen.Order < originTopOrder;
        }
        
        private bool TopScreen(Screen screen)
        {
            return screen.Order >= originTopOrder;
        }
    }
}
