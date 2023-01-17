using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class StatesInitializer : ServiceInitializer
    {
        [Inject]
        private readonly StateFactory<UniTask> _stateFactory;

        [Inject]
        private readonly IStates _states;

        public override UniTask<bool> Initialize()
        {
            BindStates();
            _states.ChangeState<ExampleState>();
            CompleteInitialize(true);
            return base.Initialize();
        }

        protected virtual void BindStates()
        {
            var exampleState = _stateFactory.Create<ExampleState>();
            _states.BindState(exampleState);
        }
    }
}
