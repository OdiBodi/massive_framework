using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class LevelsInstaller : ServiceInstaller
    {
        [SerializeField]
        private Transform _root;

        public override void InstallBindings()
        {
            Container.Bind<ILevels>().To<Levels>().AsSingle().WithArguments(_root);
            Container.BindFactory<int, Transform, Level, LevelFactory>().FromFactory<LevelCustomFactory>();
        }
    }
}
