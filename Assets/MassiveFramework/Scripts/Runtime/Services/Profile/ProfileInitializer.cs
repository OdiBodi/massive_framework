using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ProfileInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IProfile _profile;

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        public override async UniTask<bool> Initialize()
        {
            var result = await _profile.Synchronize();
            CompleteInitialize(result);
            return await base.Initialize();
        }

        protected virtual void InitializeProfileValues(IProfile profile)
        {
            var nowTime = DateTime.Now;
            profile.Property(ProfileIds.FirstLaunchDate, nowTime);
        }
    }
}
