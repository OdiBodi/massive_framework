using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class PoolInstaller : ServiceInstaller
    {
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private int _capacity = 100;

        public override void InstallBindings()
        {
            Container.Bind<IPool>().To<Pool>().AsSingle().WithArguments(_root, _capacity);
        }
    }
}
