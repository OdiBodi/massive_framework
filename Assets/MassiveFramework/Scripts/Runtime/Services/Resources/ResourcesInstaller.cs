using System;

namespace MassiveCore.Framework.Runtime
{
    public class ResourcesInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IResources>().To<Resources>().AsSingle();
            Container.BindFactory<Type, IResource, ResourceFactory>().FromFactory<ResourceCustomFactory>();
        }
    }
}
