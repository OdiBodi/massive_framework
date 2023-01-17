namespace MassiveCore.Framework.Runtime
{
    public class ApplicationReviewInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_IOS
#if DEBUG
            Container.Bind<IApplicationReview>().To<EditorApplicationReview>().AsSingle();
#else
            Container.Bind<IApplicationReview>().To<IosApplicationReview>().AsSingle();
#endif
#elif UNITY_ANDROID
#if DEBUG
            Container.Bind<IApplicationReview>().To<EditorApplicationReview>().AsSingle();
#else
            Container.Bind<IApplicationReview>().To<AndroidApplicationReview>().AsSingle();
#endif
#endif
        }
    }
}
