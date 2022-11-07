using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class SessionInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IProfile _profile; 

        public override UniTask<bool> Initialize()
        {
            InitializeSession();
            CompleteInitialize(true);
            return base.Initialize();
        }

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        private void InitializeSession()
        {
            new Session(_profile, this).Initialize();
        }

        private void InitializeProfileValues(IProfile profile)
        {
            var nowTime = DateTime.Now;
            profile.Property(ProfileIds.SessionNumber, 0);
            profile.Property(ProfileIds.LastSessionDate, nowTime);
        }
    }
}
