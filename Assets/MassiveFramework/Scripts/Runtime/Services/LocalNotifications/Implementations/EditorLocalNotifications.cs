using System;
using System.Globalization;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorLocalNotifications : ILocalNotifications
    {
        [Inject]
        private readonly ILogger _logger;

        public event Action<LocalNotification> NotificationReceived;

        public void Initialize()
        {
            _logger.Print("EditorLocalNotifications::Init");
        }

        public void Reset()
        {
            _logger.Print("EditorLocalNotifications::Reset");
        }

        public LocalNotification LastEntryNotification()
        {
            _logger.Print("EditorLocalNotifications::LastEntryNotification");
            return default;
        }

        public void ScheduleNotification(string title, string text, DateTime time)
        {
            var timeString = time.ToString(CultureInfo.InvariantCulture); 
            _logger.Print($"EditorLocalNotifications::ScheduleNotification(\"{title}\", \"{text}\", \"{timeString}\")");
        }
    }
}
