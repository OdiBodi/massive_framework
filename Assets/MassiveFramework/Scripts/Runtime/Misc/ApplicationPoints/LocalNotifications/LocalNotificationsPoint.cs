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
                var config = gameConfig.Config<LocalNotificationsConfig>();
                new LocalNotificationsScheduler(notifications, logger, config).Schedule();
            }
        }

        private void InitNotifications()
        {
            notifications.Init();
            notifications.Reset();
        }
    }
}
