using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class LevelsInitializer : ServiceInitializer
    {
        [Inject]
        protected virtual void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        public override UniTask<bool> Initialize()
        {
            CompleteInitialize(true);
            return base.Initialize();
        }

        protected virtual void InitializeProfileValues(IProfile profile)
        {
            profile.Property(ProfileIds.LevelIndex, 0);
        }
    }
}
