namespace MassiveCore.Framework.Runtime
{
    public class VisualEffectsInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IVisualEffects>().To<VisualEffects>().AsSingle();
            Container.BindFactory<string, VisualEffect, VisualEffectPlaceholderFactory>().FromFactory<VisualEffectCustomFactory>();
        }
    }
}
