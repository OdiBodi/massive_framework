using System;

namespace MassiveCore.Framework
{
    public class GameDay
    {
        private readonly IProfile profile;

        public GameDay(IProfile profile)
        {
            this.profile = profile;
        }

        public int Day()
        {
            return (int) (DateTime.Now - profile.FirstLaunchDate).TotalDays;
        }
    }
}
