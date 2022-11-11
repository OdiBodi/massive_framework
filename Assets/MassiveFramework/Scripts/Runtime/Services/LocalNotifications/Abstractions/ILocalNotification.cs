using System;

namespace MassiveCore.Framework
{
    public interface ILocalNotification
    {
        string Title { get; }
        string Text  { get; }
        DateTime Time { get; }
    }
}
