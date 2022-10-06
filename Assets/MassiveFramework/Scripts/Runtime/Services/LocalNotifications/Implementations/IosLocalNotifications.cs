#if UNITY_IOS

using System;
using System.Globalization;
using Unity.Notifications.iOS;

namespace MassiveCore.Framework
{
    public class IosLocalNotifications : ILocalNotifications
    {
        public event Action<LocalNotification> OnNotificationReceived;

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

        public LocalNotification LastEntryNotification()
        {
            var notification = iOSNotificationCenter.GetLastRespondedNotification();
            if (notification != null)
            {
                return NotificationBy(notification);
            }
            return default;
        }

        private void Subscribe()
        {
            iOSNotificationCenter.OnNotificationReceived += iosNotification =>
            {
                if (OnNotificationReceived == null)
                {
                    return;
                }
                var notification = NotificationBy(iosNotification);
                OnNotificationReceived(notification);
            };
        }

        private LocalNotification NotificationBy(iOSNotification iosNotification)
        {
            var trigger = (iOSNotificationCalendarTrigger)iosNotification.Trigger;
            var time = new DateTime(trigger.Year.GetValueOrDefault(), trigger.Month.GetValueOrDefault(),
                trigger.Day.GetValueOrDefault(), trigger.Hour.GetValueOrDefault(), trigger.Minute.GetValueOrDefault(),
                trigger.Second.GetValueOrDefault());
            var notification = new LocalNotification
            {
                title = iosNotification.Title,
                text = iosNotification.Body,
                time = time
            };
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
