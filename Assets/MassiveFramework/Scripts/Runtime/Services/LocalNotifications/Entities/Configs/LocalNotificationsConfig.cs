using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "local_notifications_config", menuName = "Massive Framework/Configs/Local Notifications Config")]
    public class LocalNotificationsConfig : Config
    {
        [SerializeField]
        private LocalNotificationConfig[] _configs;

        public LocalNotificationConfig[] Configs => _configs;
    }
}
