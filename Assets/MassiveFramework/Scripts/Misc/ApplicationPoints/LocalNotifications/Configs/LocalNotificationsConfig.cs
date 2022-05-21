using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "local_notifications_config", menuName = "Massive Framework/Configs/Local Notifications Config")]
    public class LocalNotificationsConfig : Config
    {
        [SerializeField]
        private LocalNotificationConfig[] configs;

        public LocalNotificationConfig[] Configs => configs;
    }
}
