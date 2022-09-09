using Zenject;

namespace MassiveCore.Framework
{
    public class ServiceInstaller : MonoInstaller, IServiceInstaller
    {
        public MonoInstaller Installer()
        {
            return this;
        }
    }
}
