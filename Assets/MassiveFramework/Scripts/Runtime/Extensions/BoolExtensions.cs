namespace MassiveCore.Framework
{
    public static class BoolExtensions
    {
        public static string ToNumberString(this bool value)
        {
            return value ? "1" : "0";
        }
    }
}
