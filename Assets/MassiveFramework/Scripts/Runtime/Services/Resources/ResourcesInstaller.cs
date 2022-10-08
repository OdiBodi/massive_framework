using System;

namespace MassiveCore.Framework
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
