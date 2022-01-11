using System;

namespace MassiveCore.Framework
{
    public static class ProfileExtensions
    {
        public static int GameDay(this IProfile profile)
        {
            return (int) (DateTime.Now - profile.FirstLaunchDate).TotalDays;
        }
    }
}
