using System;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ScreensInstaller : ServiceInstaller
    {
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private int _originTopOrder = 100;

        public override void InstallBindings()
        {
            Container.Bind<IScreens>().To<Screens>().AsSingle().WithArguments(_root, _originTopOrder);
            Container.BindFactory<Type, Transform, Screen, ScreenFactory>().FromFactory<ScreenCustomFactory>();
        }
    }
}
