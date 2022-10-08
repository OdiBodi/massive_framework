using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class SessionInitializer : ServiceInitializer
    {
        public override UniTask<bool> Initialize()
        {
            CompleteInitialize(true);
            return base.Initialize();
        }

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.OnPreLoading += () => InitializeProfileValues(profile);
        }

        private void InitializeProfileValues(IProfile profile)
        {
            var nowTime = DateTime.Now;
            profile.Property(ProfileIds.SessionNumber, -1);
            profile.Property(ProfileIds.LastSessionDate, nowTime);
        }
    }
}
