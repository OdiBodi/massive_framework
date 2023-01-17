using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public class Level : BaseMonoBehaviour, ILevel
    {
        private ReactiveProperty<bool> _loaded;

        public ReadOnlyReactiveProperty<bool> Loaded { get; private set; }

        protected virtual void Awake()
        {
            InitializeLoadedReactiveProperties();
        }

        protected virtual void Start()
        {
            CompleteLoad();
        }

        private void InitializeLoadedReactiveProperties()
        {
            _loaded = new ReactiveProperty<bool>();
            Loaded = _loaded.ToReadOnlyReactiveProperty();
        }

        protected void CompleteLoad()
        {
            _loaded.Value = true;
        }
    }
}
