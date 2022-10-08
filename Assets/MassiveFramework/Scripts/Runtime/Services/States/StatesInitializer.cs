using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class StatesInitializer : ServiceInitializer
    {
        [Inject]
        private readonly StateFactory _stateFactory;

        [Inject]
        private readonly IStates _states;

        public override UniTask<bool> Initialize()
        {
            BindStates();
            _states.GoTo<ExampleState>();
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
