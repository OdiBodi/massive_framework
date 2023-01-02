using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class ApplicationReviewInitializer : ServiceInitializer
    {
        [Inject]
        private void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        public override UniTask<bool> Initialize()
        {
            CompleteInitialize(true);
            return base.Initialize();
        }

        private void InitializeProfileValues(IProfile profile)
        {
            profile.Property(ProfileIds.ApplicationReviewActive, true);
        }
    }
}
