using System;
using System.Globalization;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "local_notification_config", menuName = "Massive Framework/Configs/Local Notification Config")]
    public class LocalNotificationConfig : ScriptableObject
    {
        [SerializeField]
        private string title;

        [SerializeField]
        private string text;

        [SerializeField]
        private string time = "12:00";

        public string Title => title;
        public string Text => text;
        public TimeSpan TimeSpan => TimeSpan.ParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture);
    }
}
