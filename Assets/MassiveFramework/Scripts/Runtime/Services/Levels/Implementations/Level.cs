using System;
using Zenject;

namespace MassiveCore.Framework
{
    public class Level : BaseMonoBehaviour
    {
        public class Factory : PlaceholderFactory<int, Level>
        {
        }

        public event Action OnLoaded;

        private void Start()
        {
            OnLoaded?.Invoke();
        }
    }
}
