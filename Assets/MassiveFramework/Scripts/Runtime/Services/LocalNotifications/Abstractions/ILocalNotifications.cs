using System;

namespace MassiveCore.Framework
{
    public interface ILocalNotifications
    {
        event Action<LocalNotification> NotificationReceived;
        void Initialize();
        void Reset();
        LocalNotification LastEntryNotification();
        void ScheduleNotification(string title, string text, DateTime time);
    }
}
