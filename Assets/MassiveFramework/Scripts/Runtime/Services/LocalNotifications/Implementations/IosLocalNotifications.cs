#if UNITY_IOS

using System;
using System.Globalization;
using Unity.Notifications.iOS;

namespace MassiveCore.Framework
{
    public class IosLocalNotifications : ILocalNotifications
    {
        public event Action<ILocalNotification> NotificationReceived;

        public void Initialize()
        {
            Subscribe();
        }

        public void Reset()
        {
            iOSNotificationCenter.RemoveAllDeliveredNotifications();
            iOSNotificationCenter.RemoveAllScheduledNotifications();
            iOSNotificationCenter.ApplicationBadge = 0;
        }

        public ILocalNotification LastEntryNotification()
        {
            var notification = iOSNotificationCenter.GetLastRespondedNotification();
            if (notification == null)
            {
                return null;
            }
            return NotificationBy(notification);
        }

        private void Subscribe()
        {
            iOSNotificationCenter.OnNotificationReceived += iosNotification =>
            {
                if (NotificationReceived == null)
                {
                    return;
                }
                var notification = NotificationBy(iosNotification);
                NotificationReceived(notification);
            };
        }

        private ILocalNotification NotificationBy(iOSNotification iosNotification)
        {
            var title = iosNotification.Title;
            var text = iosNotification.Body;

            var trigger = (iOSNotificationCalendarTrigger) iosNotification.Trigger;
            var year = trigger.Year.GetValueOrDefault();
            var month = trigger.Month.GetValueOrDefault();
            var day = trigger.Day.GetValueOrDefault();
            var hour = trigger.Hour.GetValueOrDefault();
            var minute = trigger.Minute.GetValueOrDefault();
            var second = trigger.Second.GetValueOrDefault(); 
            var time = new DateTime(year, month, day, hour, minute, second);

            var notification = new LocalNotification(title, text, time);
            return notification;
        }

        public void ScheduleNotification(string title, string text, DateTime time)
        {
            var trigger = new iOSNotificationCalendarTrigger
            {
                Year = time.Year,
                Month = time.Month,
                Day = time.Day,
                Hour = time.Hour,
                Minute = time.Minute,
                Second = time.Second,
                Repeats = false
            };
            var notification = new iOSNotification
            {
                Identifier = time.ToString(CultureInfo.InvariantCulture),
                CategoryIdentifier = "general",
                ThreadIdentifier = "thread0",
                Title = title,
                Body = text,
                ShowInForeground = false,
                Badge = 1,
                Trigger = trigger
            };
            iOSNotificationCenter.ScheduleNotification(notification);
        }
    }
}

#endif // UNITY_IOS
