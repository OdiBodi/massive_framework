using UnityEngine;

namespace MassiveCore.Framework
{
    public class PoolInstaller : ServiceInstaller
    {
        [SerializeField]
        private Pool _pool;

        public override void InstallBindings()
        {
            Container.Bind<IPool>().FromInstance(_pool).AsSingle();
        }
    }
}
