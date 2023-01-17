using System;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;

namespace MassiveCore.Framework.Runtime
{
    public class CustomJsonTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                var json = value.ToString();
                var result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
