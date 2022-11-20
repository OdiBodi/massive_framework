using UnityEngine;

namespace MassiveCore.Framework
{
    public class YandexInstaller : ServiceInstaller
    {
        [SerializeField]
        private Yandex _yandex;

        public override void InstallBindings()
        {
            Container.Bind<IYandex>().FromInstance(_yandex).AsSingle();
        }
    }
}
