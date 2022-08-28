using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Linq;

namespace MassiveCore.Framework
{
    public class EntryPoint : BaseMonoBehaviour
    {
        public event Action OnLoaded;

        public bool Loaded { get; private set; }

        private IEnumerable<ApplicationPoint> Points => CacheGameObject.Descendants().OfComponent<ApplicationPoint>()
            .Where(point => point.Activity());

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
