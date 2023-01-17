using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ServiceInstaller : MonoInstaller, IServiceInstaller
    {
        public MonoInstaller Installer()
        {
            return this;
        }
    }
}
