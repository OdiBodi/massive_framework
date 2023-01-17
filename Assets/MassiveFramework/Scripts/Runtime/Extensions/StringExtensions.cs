using System.ComponentModel;

namespace MassiveCore.Framework.Runtime
{
    public static class StringExtensions
    {
        public static T As<T>(this string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            var output = (T)converter.ConvertFromInvariantString(value);
            return output;
        }
    }
}
