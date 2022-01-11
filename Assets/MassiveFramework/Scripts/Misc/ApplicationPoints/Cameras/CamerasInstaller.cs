using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class CamerasInstaller : MonoInstaller
    {
        [SerializeField]
        private Camera[] cameras;

        public override void InstallBindings()
        {
            Container.Bind<Cameras>().ToSelf().AsSingle().WithArguments(cameras);
        }
    }
}
