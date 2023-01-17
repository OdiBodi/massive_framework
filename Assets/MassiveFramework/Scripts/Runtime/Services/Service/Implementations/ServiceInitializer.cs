using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public class ServiceInitializer : BaseMonoBehaviour, IServiceInitializer
    {
        private readonly AsyncSubject<bool> _initializerSubject = new();

        public ReadOnlyReactiveProperty<bool> Initialized { get; private set; }

        private void Awake()
        {
            InitializeInitializedReactiveProperty();
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

        private void InitializeInitializedReactiveProperty()
        {
            Initialized = _initializerSubject.ToReadOnlyReactiveProperty();
        }
    }
}
