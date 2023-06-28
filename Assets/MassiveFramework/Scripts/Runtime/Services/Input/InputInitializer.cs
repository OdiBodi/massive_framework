using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class InputInitializer : ServiceInitializer
    {
        [SerializeField]
        private bool _multitouch;

        public override UniTask<bool> Initialize()
        {
            Input.multiTouchEnabled = _multitouch;
            CompleteInitialize(true);
            return base.Initialize();
        }
    }
}
