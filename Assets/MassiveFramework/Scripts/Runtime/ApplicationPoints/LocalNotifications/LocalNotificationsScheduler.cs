using System;
using System.Globalization;

namespace MassiveCore.Framework
{
    public class LocalNotificationsScheduler
    {
        private readonly ILocalNotifications notifications;
        private readonly ILogger logger;
        private readonly LocalNotificationsConfig notificationsConfig;

        public LocalNotificationsScheduler(ILocalNotifications notifications, ILogger logger, LocalNotificationsConfig notificationsConfig)
        {
            this.notifications = notifications;
            this.logger = logger;
            this.notificationsConfig = notificationsConfig;
        }

        public void Schedule()
        {
            var nowTime = DateTime.Now;
            var notifications = notificationsConfig.Configs;

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
            var time = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, notificationTimeSpan.Hours,
                notificationTimeSpan.Minutes, notificationTimeSpan.Seconds);
            time += TimeSpan.FromDays(days);
            return time;
        }

        private void ScheduleNotification(LocalNotificationConfig notificationConfig, DateTime time)
        {
            var title = notificationConfig.Title;
            var text = notificationConfig.Text;
            notifications.ScheduleNotification(title, text, time);
        }

        private void LogScheduleNotification(int day, LocalNotificationConfig notificationConfig, DateTime time)
        {
            var title = notificationConfig.Title;
            var text = notificationConfig.Text;
            var timeString = time.ToString(CultureInfo.InvariantCulture); 
            logger.Print($"Schedule Local Notification: day {day} - \"{title}\" - \"{text}\" - \"{timeString}\"");
        }
    }
}
