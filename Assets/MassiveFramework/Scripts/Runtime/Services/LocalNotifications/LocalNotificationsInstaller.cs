namespace MassiveCore.Framework
{
    public class LocalNotificationsInstaller : ServiceInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.Bind<ILocalNotifications>().To<EditorLocalNotifications>().AsSingle();
#elif UNITY_IOS
            Container.Bind<ILocalNotifications>().To<IosLocalNotifications>().AsSingle();
#elif UNITY_ANDROID
            Container.Bind<ILocalNotifications>().To<AndroidLocalNotifications>().AsSingle();
#endif
        }
    }
}
