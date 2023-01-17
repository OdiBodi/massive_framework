using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class SoundsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IPool _pool;

        [Inject]
        private readonly SoundFactory _soundFactory;

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        public override UniTask<bool> Initialize()
        {
            BindPoolFactories();
            CompleteInitialize(true);
            return base.Initialize();
        }

        protected virtual void BindPoolFactories()
        {
            var factory = new SoundPoolFactory(_soundFactory);
            (_pool as Pool).BindFactory<Sound>(factory);
        }

        private void InitializeProfileValues(IProfile profile)
        {
            profile.Property(ProfileIds.SoundsEnabled, true);
            profile.Property(ProfileIds.MusicEnabled, true);
        }
    }
}
