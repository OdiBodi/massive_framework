using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class FramerateInitializer : ServiceInitializer
    {
        [SerializeField]
        private Framerate rate = Framerate._60;

        public override async UniTask<bool> Initialize()
        {
            Application.targetFrameRate = rate.Number();
            return true;
        }
    }
}
