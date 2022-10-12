using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class SoundsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IPool _pool;

        [Inject]
        private readonly SoundFactory _soundFactory;

        public override UniTask<bool> Initialize()
        {
            BindPoolFactories();
            CompleteInitialize(true);
            return base.Initialize();
        }

        [Inject]
        private void Inject(IProfile profile)
        {
            profile.PreLoading += () => InitializeProfileValues(profile);
        }

        protected virtual void BindPoolFactories()
        {
            var factory = new SoundPoolFactory(_soundFactory);
            (_pool as Pool).BindFactory<Sound>(factory);
        }

        private void InitializeProfileValues(IProfile profile)
        {
            profile.Property(ProfileIds.SoundEnabled, true);
        }
    }
}
