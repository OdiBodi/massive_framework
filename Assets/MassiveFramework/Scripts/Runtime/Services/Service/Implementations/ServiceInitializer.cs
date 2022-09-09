using Cysharp.Threading.Tasks;
using UniRx;

namespace MassiveCore.Framework
{
    public class ServiceInitializer : BaseMonoBehaviour, IServiceInitializer
    {
        private readonly AsyncSubject<bool> initializerSubject = new AsyncSubject<bool>();

        public ReadOnlyReactiveProperty<bool> Initialized { get; private set; }

        private void Awake()
        {
            InitInitializedReactiveProperty();
        }

        public virtual UniTask<bool> Initialize()
        {
            return initializerSubject.ToUniTask();
        }

        protected void CompleteInitialize()
        {
            initializerSubject.OnNext(true);
            initializerSubject.OnCompleted();
        }
        
        private void InitInitializedReactiveProperty()
        {
            var initializerReadOnlyProperty = initializerSubject.ToReadOnlyReactiveProperty();
            Initialized = new ReadOnlyReactiveProperty<bool>(initializerReadOnlyProperty);
        }
    }
}
