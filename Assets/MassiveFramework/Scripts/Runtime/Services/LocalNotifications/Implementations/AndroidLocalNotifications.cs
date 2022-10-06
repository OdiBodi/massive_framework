#if UNITY_ANDROID

using System;
using System.Linq;
using Unity.Notifications.Android;

namespace MassiveCore.Framework
{
    public class AndroidLocalNotifications : ILocalNotifications
    {
        private const string ChannelId = "generic";

        public event Action<LocalNotification> OnNotificationReceived;

        public void Initialize()
        {
            AndroidNotificationCenter.Initialize();
            TryToRegisterGenericNotificationChannel();
            Subscribe();
        }

        public void Reset()
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }

        public LocalNotification LastEntryNotification()
        {
            var notificationIntent = AndroidNotificationCenter.GetLastNotificationIntent();
            if (notificationIntent == null)
            {
                return default;
            }
            var androidNotification = notificationIntent.Notification; 
            return NotificationBy(androidNotification);
        }

        private void TryToRegisterGenericNotificationChannel()
        {
            var channels = AndroidNotificationCenter.GetNotificationChannels();
            if (channels.Any(channel => channel.Id == ChannelId))
            {
                return;
            }
            var channel = new AndroidNotificationChannel
            {
                Id = ChannelId,
                Name = "Generic",
                Description = "Generic",
                Importance = Importance.High,
                CanShowBadge = true,
                EnableLights = true,
                EnableVibration = true,
                LockScreenVisibility = LockScreenVisibility.Public
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        private void Subscribe()
        {
            AndroidNotificationCenter.OnNotificationReceived += data =>
            {
                if (OnNotificationReceived == null)
                {
                    return;
                }
                var androidNotification = data.Notification;
                var notification = NotificationBy(androidNotification);
                OnNotificationReceived(notification);
            };
        }

        private LocalNotification NotificationBy(AndroidNotification androidNotification)
        {
            var notification = new LocalNotification
            {
                title = androidNotification.Title,
                text = androidNotification.Text,
                time = androidNotification.FireTime
            };
            return notification;
        }

        public void ScheduleNotification(string title, string text, DateTime time)
        {
            var notification = new AndroidNotification
            {
                Title = title,
                Text = text,
                FireTime = time,
                Group = ChannelId,
                SmallIcon = "small_icon",
                LargeIcon = "large_icon"
            };
            AndroidNotificationCenter.SendNotification(notification, ChannelId);
        }
    }
}

#endif // UNITY_ANDROID
