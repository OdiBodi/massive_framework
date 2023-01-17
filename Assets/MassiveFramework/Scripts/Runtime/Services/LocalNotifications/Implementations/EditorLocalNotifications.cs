using System;
using System.Globalization;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class EditorLocalNotifications : ILocalNotifications
    {
        [Inject]
        private readonly ILogger _logger;

        public event Action<ILocalNotification> NotificationReceived;

        public void Initialize()
        {
            _logger.Print("EditorLocalNotifications initialized!");
        }

        public void Reset()
        {
            _logger.Print("EditorLocalNotifications reset!");
        }

        public ILocalNotification LastEntryNotification()
        {
            return null;
        }

        public void ScheduleNotification(string title, string text, DateTime time)
        {
            var timeString = time.ToString(CultureInfo.InvariantCulture); 
            _logger.Print($"EditorLocalNotifications schedule notification: \"{title}\", \"{text}\", \"{timeString}");
        }
    }
}
