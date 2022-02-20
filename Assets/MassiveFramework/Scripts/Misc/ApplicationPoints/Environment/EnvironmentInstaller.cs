using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class EnvironmentInstaller : MonoInstaller
    {
        [SerializeField]
        private Light directionalLight;

        public override void InstallBindings()
        {
            Container.Bind<Environment>().ToSelf().AsSingle().WithArguments(directionalLight);
        }
    }
}
