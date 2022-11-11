using System;
using System.Globalization;

namespace MassiveCore.Framework
{
    public class LocalNotificationsScheduler
    {
        private readonly ILocalNotifications _notifications;
        private readonly ILogger _logger;
        private readonly LocalNotificationsConfig _notificationsConfig;

        public LocalNotificationsScheduler(ILocalNotifications notifications, ILogger logger,
            LocalNotificationsConfig notificationsConfig)
        {
            _notifications = notifications;
            _logger = logger;
            _notificationsConfig = notificationsConfig;
        }

        public void Schedule()
        {
            var nowTime = DateTime.Now;
            var notifications = _notificationsConfig.Configs;

            for (var i = 0; i < notifications.Length; ++i)
            {
                var notification = notifications[i];
                var notificationTime = NotificationTime(nowTime, notification.TimeSpan, i);

                var hours = (notificationTime - nowTime).TotalHours;
                if (hours < 2f)
                {
                    continue;
                }

                ScheduleNotification(notification, notificationTime);
                LogScheduleNotification(i, notification, notificationTime);
            }
        }

        private DateTime NotificationTime(DateTime nowTime, TimeSpan notificationTimeSpan, int days)
        {
            var year = nowTime.Year;
            var month = nowTime.Month;
            var day = nowTime.Day;
            var hour = notificationTimeSpan.Hours;
            var minute = notificationTimeSpan.Minutes;
            var second = notificationTimeSpan.Seconds;
            var time = new DateTime(year, month, day, hour, minute, second);
            time += TimeSpan.FromDays(days);
            return time;
        }

        private void ScheduleNotification(LocalNotificationConfig notificationConfig, DateTime time)
        {
            var title = notificationConfig.Title;
            var text = notificationConfig.Text;
            _notifications.ScheduleNotification(title, text, time);
        }

        private void LogScheduleNotification(int day, LocalNotificationConfig notificationConfig, DateTime time)
        {
            var title = notificationConfig.Title;
            var text = notificationConfig.Text;
            var timeString = time.ToString(CultureInfo.InvariantCulture); 
            _logger.Print($"Schedule local notification: day {day} - \"{title}\" - \"{text}\" - \"{timeString}\"");
        }
    }
}
