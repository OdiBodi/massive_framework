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

        private DateTime FirstLaunchDate => profile.Property<DateTime>(ProfileIds.FirstLaunchDate).Value;

        public int Day()
        {
            return (int) (DateTime.Now - FirstLaunchDate).TotalDays;
        }
    }
}
