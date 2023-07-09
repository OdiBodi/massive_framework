using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class SoundsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IPool _pool;

        [Inject]
        private readonly SoundPlaceholderFactory _soundPlaceholderFactory;

        [SerializeField]
        private Transform _root;

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
            var factory = new SoundAbstractFactory(_soundPlaceholderFactory);
            (_pool as Pool).BindObjectPool<Sound>(factory, null, _root, 100);
        }

        private void InitializeProfileValues(IProfile profile)
        {
            profile.Property(ProfileIds.SoundsEnabled, true);
            profile.Property(ProfileIds.MusicEnabled, true);
        }
    }
}
