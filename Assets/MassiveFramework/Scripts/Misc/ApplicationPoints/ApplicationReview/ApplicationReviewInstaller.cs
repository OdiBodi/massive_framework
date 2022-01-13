using Zenject;

namespace MassiveCore.Framework
{
    public class ApplicationReviewInstaller : MonoInstaller
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
