using System;

namespace MassiveCore.Framework.Runtime
{
    public class LocalNotification : ILocalNotification
    {
        public LocalNotification(string title, string text, DateTime time)
        {
            Title = title;
            Text = text;
            Time = time;
        }

        public string Title { get; }
        public string Text { get; }
        public DateTime Time { get; }
    }
}
