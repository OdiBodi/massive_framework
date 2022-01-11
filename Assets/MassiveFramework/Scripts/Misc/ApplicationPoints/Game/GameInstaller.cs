using Zenject;

namespace MassiveCore.Framework
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Game>().ToSelf().AsSingle();
        }
    }
}
