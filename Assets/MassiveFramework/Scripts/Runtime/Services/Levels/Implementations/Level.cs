using System;

namespace MassiveCore.Framework
{
    public class Level : BaseMonoBehaviour, ILevel
    {
        public event Action Loaded;

        protected virtual void Start()
        {
            Loaded?.Invoke();
        }
    }
}
