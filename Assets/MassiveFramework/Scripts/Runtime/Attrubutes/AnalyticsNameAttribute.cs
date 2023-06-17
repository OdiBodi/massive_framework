using System;

namespace MassiveCore.Framework.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AnalyticsNameAttribute : Attribute
    {
        public AnalyticsNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
