using System;
using System.Collections.Generic;
using Unity.Linq;

namespace MassiveCore.Framework
{
    public class EntryPoint : BaseMonoBehaviour
    {
        public event Action OnLoaded;

        public bool Loaded { get; private set; }

        private IEnumerable<ApplicationPoint> Points => CacheGameObject.Descendants().OfComponent<ApplicationPoint>();

        private void Awake()
        {
            ActivatePoints();
        }

        private async void ActivatePoints()
        {
            foreach (var point in Points)
            {
                point.Init();
                await point.WaitForComplete();
            }
            Loaded = true;
            OnLoaded?.Invoke();
        }
    }
}
