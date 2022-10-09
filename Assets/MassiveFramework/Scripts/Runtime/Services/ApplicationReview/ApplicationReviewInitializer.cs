using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class ApplicationReviewInitializer : ServiceInitializer
    {
        public override UniTask<bool> Initialize()
        {
            CompleteInitialize(true);
            return base.Initialize();
        }

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        private void InitializeProfileValues(IProfile profile)
        {
            profile.Property(ProfileIds.ApplicationReviewActive, true);
        }
    }
}
