using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class FramerateInitializer : ServiceInitializer
    {
        [SerializeField]
        private Framerate _rate = Framerate._60;

        public override UniTask<bool> Initialize()
        {
            Application.targetFrameRate = _rate.Number();
            CompleteInitialize(true);
            return base.Initialize();
        }
    }
}
