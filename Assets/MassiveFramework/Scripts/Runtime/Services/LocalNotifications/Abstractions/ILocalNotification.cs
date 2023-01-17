using System;

namespace MassiveCore.Framework.Runtime
{
    public interface ILocalNotification
    {
        string Title { get; }
        string Text  { get; }
        DateTime Time { get; }
    }
}
