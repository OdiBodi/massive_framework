using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class LevelsInitializer : ServiceInitializer
    {
        public override UniTask<bool> Initialize()
        {
            CompleteInitialize(true);
            return base.Initialize();
        }

        [Inject]
        protected virtual void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        protected virtual void InitializeProfileValues(IProfile profile)
        {
            profile.Property(ProfileIds.LevelIndex, 0);
        }
    }
}
