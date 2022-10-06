using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class ScreensInstaller : ServiceInstaller
    {
        [Inject]
        private readonly IGameConfig _gameConfig;

        [SerializeField]
        private Transform _root;

        [SerializeField]
        private int _originTopOrder = 100;

        private ScreensConfig ScreensConfig => _gameConfig.Config<ScreensConfig>();

        public override void InstallBindings()
        {
            Container.Bind<IScreens>().To<Screens>().AsSingle().WithArguments(_root, _originTopOrder);
            Container.BindFactory<Type, Screen, Screen.Factory>().FromMethod
            (
                (c, type) =>
                {
                    var configs = ScreensConfig.Configs; 
                    var prefab = configs.First(x => x.Prefab.GetType() == type).Prefab;
                    var screen = c.InstantiatePrefabForComponent<Screen>(prefab);
                    screen.name = prefab.name;
                    return screen;
                }
            );
        }
    }
}
