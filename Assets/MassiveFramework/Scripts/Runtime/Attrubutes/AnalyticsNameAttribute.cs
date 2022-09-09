using System;

namespace MassiveCore.Framework
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AnalyticsNameAttribute : Attribute
    {
        public string Name { get; }
        public AnalyticsNameAttribute(string name)
        {
            Name = name;
        }
    }
}
