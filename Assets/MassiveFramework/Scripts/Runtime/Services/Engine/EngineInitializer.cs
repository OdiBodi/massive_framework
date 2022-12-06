using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace MassiveCore.Framework
{
    public class EngineInitializer : ServiceInitializer
    {
        public override UniTask<bool> Initialize()
        {
#if UNITY_EDITOR
            CompleteInitialize(true);
#endif
            return base.Initialize();
        }

        [Preserve]
        public void OnInitialized()
        {
            CompleteInitialize(true);
        }
    }
}
