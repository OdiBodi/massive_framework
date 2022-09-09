using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class ProfileInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IProfile profile;

        public override async UniTask<bool> Initialize()
        {
            InitializeProfileValues();
            profile.Sync();
            return true;
        }

        private void InitializeProfileValues()
        {
            var nowTime = DateTime.Now;

            profile.Property(ProfileIds.FirstLaunchDate, nowTime);

            profile.Property(ProfileIds.SessionNumber, -1);
            profile.Property(ProfileIds.LastSessionDate, nowTime);

            profile.Property(ProfileIds.ApplicationReviewActive, true);

            profile.Property(ProfileIds.LevelIndex, 0);
        }
    }
}
