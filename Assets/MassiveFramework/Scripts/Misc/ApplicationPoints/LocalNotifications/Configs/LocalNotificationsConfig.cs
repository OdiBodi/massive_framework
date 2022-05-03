using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "local_notifications_config", menuName = "Massive Framework/Configs/Local Notifications Config")]
    public class LocalNotificationsConfig : ScriptableObject
    {
        [SerializeField]
        private LocalNotificationConfig[] configs;

        public LocalNotificationConfig[] Configs => configs;
    }
}
