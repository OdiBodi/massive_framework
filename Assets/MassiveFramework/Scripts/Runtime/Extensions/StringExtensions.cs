using System.ComponentModel;
using UnityEngine;

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

        public static Color Color(this string value)
        {
            ColorUtility.TryParseHtmlString(value, out var color);
            return color;
        }
    }
}
