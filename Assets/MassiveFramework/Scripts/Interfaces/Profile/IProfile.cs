using System;

namespace MassiveCore.Framework
{
    public interface IProfile
    {
        DateTime FirstLaunchDate { get; }
        DateTime LastSessionDate { get; set; }

        int NumberSession { get; set; }

        void Sync();
    }
}
