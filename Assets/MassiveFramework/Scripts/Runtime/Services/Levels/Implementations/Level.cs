using System;
using Zenject;

namespace MassiveCore.Framework
{
    public class Level : BaseMonoBehaviour
    {
        public class Factory : PlaceholderFactory<int, Level>
        {
        }

        public event Action Loaded;

        private void Start()
        {
            Loaded?.Invoke();
        }
    }
}
