using System;
using System.Reflection;

namespace MassiveCore.Framework
{
    public static class EnumExtensions
    {
        public static string AnalyticsName(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name).GetCustomAttribute<AnalyticsNameAttribute>(false)?.Name ?? string.Empty;
        }

        public static int Number(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name).GetCustomAttribute<NumberAttribute>(false)?.Number ?? -1;
        }
    }
}
