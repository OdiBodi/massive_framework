using System;

namespace MassiveCore.Framework
{
    public class Level : BaseMonoBehaviour
    {
        public event Action Loaded;

        private void Start()
        {
            Loaded?.Invoke();
        }
    }
}
