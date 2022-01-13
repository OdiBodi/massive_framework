using System;
using UniRx;

namespace MassiveCore.Framework
{
    public interface IProfile
    {
        DateTime FirstLaunchDate { get; }
        DateTime LastSessionDate { get; set; }

        int NumberSession { get; set; }

        ReactiveProperty<bool> ApplicationReviewActive { get; }

        ReactiveProperty<int> LevelIndex { get; } 

        void Sync();
    }
}
