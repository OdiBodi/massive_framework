using System;

namespace MassiveCore.Framework.Runtime
{
    public static class DateTimeExtensions
    {
        public static string ToBinaryString(this DateTime dateTime)
        {
            return dateTime.ToBinary().ToString(); 
        }
    }
}
