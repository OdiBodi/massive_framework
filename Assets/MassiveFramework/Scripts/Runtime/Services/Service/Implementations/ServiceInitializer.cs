using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework
{
    public class ServiceInitializer : BaseMonoBehaviour, IServiceInitializer
    {
        private readonly AsyncSubject<bool> _initializerSubject = new();

        public ReadOnlyReactiveProperty<bool> Initialized { get; private set; }

        private void Awake()
        {
            InitInitializedReactiveProperty();
        }

        public virtual UniTask<bool> Initialize()
        {
            return _initializerSubject.ToUniTask();
        }

        protected void CompleteInitialize(bool result)
        {
            _initializerSubject.OnNext(result);
            _initializerSubject.OnCompleted();
        }

        private void InitInitializedReactiveProperty()
        {
            var initializerReadOnlyProperty = _initializerSubject.ToReadOnlyReactiveProperty();
            Initialized = new ReadOnlyReactiveProperty<bool>(initializerReadOnlyProperty);
        }
    }
}
