using System;

namespace MassiveCore.Framework
{
    public class GameDay
    {
        private readonly IProfile _profile;

        public GameDay(IProfile profile)
        {
            _profile = profile;
        }

        private DateTime FirstLaunchDate => _profile.Property<DateTime>(ProfileIds.FirstLaunchDate).Value;

        public int Day()
        {
            var day = (int)(DateTime.Now - FirstLaunchDate).TotalDays;
            return day;
        }
    }
}
