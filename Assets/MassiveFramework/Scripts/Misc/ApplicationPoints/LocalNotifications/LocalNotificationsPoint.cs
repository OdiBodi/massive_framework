using Zenject;

namespace MassiveCore.Framework
{
    public class LocalNotificationsPoint : ApplicationPoint
    {
        [Inject]
        private readonly ILocalNotifications notifications;

        [Inject]
        private readonly GameConfig gameConfig;

        public override void Init()
        {
            InitNotifications();
            Complete();
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            if (notifications == null)
            {
                return;
            }
            if (hasFocus)
            {
                notifications.Reset();
            }
            else
            {
                new LocalNotificationsScheduler(notifications, logger, gameConfig.LocalNotificationsConfig).Schedule();
            }
        }

        private void InitNotifications()
        {
            notifications.Init();
            notifications.Reset();
        }
    }
}
