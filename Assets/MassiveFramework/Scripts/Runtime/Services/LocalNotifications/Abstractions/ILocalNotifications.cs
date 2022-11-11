using System;

namespace MassiveCore.Framework
{
    public interface ILocalNotifications
    {
        event Action<ILocalNotification> NotificationReceived;
        void Initialize();
        void Reset();
        ILocalNotification LastEntryNotification();
        void ScheduleNotification(string title, string text, DateTime time);
    }
}
