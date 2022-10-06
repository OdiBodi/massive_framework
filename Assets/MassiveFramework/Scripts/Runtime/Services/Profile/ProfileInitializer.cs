using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class ProfileInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IProfile _profile;

        public override UniTask<bool> Initialize()
        {
            InitializeProfileValues();
            _profile.Sync();
            CompleteInitialize(true);
            return base.Initialize();
        }

        private void InitializeProfileValues()
        {
            var nowTime = DateTime.Now;

            _profile.Property(ProfileIds.FirstLaunchDate, nowTime);

            _profile.Property(ProfileIds.SessionNumber, -1);
            _profile.Property(ProfileIds.LastSessionDate, nowTime);

            _profile.Property(ProfileIds.ApplicationReviewActive, true);

            _profile.Property(ProfileIds.LevelIndex, 0);
        }
    }
}
