using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class ScreensInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform root;

        [SerializeField]
        private int originTopOrder = 100;

        [SerializeField]
        private Screen[] screenPrefabs;

        public override void InstallBindings()
        {
            Container.Bind<Screens>().To<Screens>().AsSingle().WithArguments(root, originTopOrder);
            Container.BindFactory<Type, Screen, Screen.Factory>().FromMethod
            (
                (c, type) =>
                {
                    var prefab = screenPrefabs.First(x => x.GetType() == type);
                    var screen = c.InstantiatePrefabForComponent<Screen>(prefab);
                    screen.name = prefab.name;
                    return screen;
                }
            );
        }
    }
}
