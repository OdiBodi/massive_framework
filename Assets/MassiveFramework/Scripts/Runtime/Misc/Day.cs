using System;

namespace MassiveCore.Framework.Runtime
{
    public class Day
    {
        private readonly IProfile _profile;

        public Day(IProfile profile)
        {
            _profile = profile;
        }

        private DateTime FirstLaunchDate => _profile.Property<DateTime>(ProfileIds.FirstLaunchDate).Value;

        public int Current()
        {
            var day = (int)(DateTime.Now - FirstLaunchDate).TotalDays;
            return day;
        }
    }
}
