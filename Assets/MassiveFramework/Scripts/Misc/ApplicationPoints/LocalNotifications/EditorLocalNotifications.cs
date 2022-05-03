using System;
using System.Globalization;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorLocalNotifications : ILocalNotifications
    {
        [Inject]
        private readonly ILogger logger;

        public event Action<LocalNotification> OnNotificationReceived;

        public void Init()
        {
            logger.Print("EditorLocalNotifications::Init");
        }

        public void Reset()
        {
            logger.Print("EditorLocalNotifications::Reset");
        }

        public LocalNotification LastEntryNotification()
        {
            logger.Print("EditorLocalNotifications::LastEntryNotification");
            return default;
        }

        public void ScheduleNotification(string title, string text, DateTime time)
        {
            var timeString = time.ToString(CultureInfo.InvariantCulture); 
            logger.Print($"EditorLocalNotifications::ScheduleNotification(\"{title}\", \"{text}\", \"{timeString}\")");
        }
    }
}
