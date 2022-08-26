using System;

namespace MassiveCore.Framework
{
    public interface ILocalNotifications
    {
        event Action<LocalNotification> OnNotificationReceived;
        void Init();
        void Reset();
        LocalNotification LastEntryNotification();
        void ScheduleNotification(string title, string text, DateTime time);
    }
}
