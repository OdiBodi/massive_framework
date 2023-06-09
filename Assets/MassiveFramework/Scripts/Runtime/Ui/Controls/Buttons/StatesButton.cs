using System;
using System.Linq;

namespace MassiveCore.Framework.Runtime
{
    public class StatesButton : StatesControl<Button>
    {
        public event Action<State> Clicked;

        protected override void Awake()
        {
            Subscribe();
            base.Awake();
        }

        private void Subscribe()
        {
            this.ForEach(state => state.control.Clicked += () => Clicked?.Invoke(state));
        }

        public T Button<T>(string id)
            where T : Button 
        {
            return (T)this.First(state => state.id == id).control;
        }

        public T CurrentButton<T>()
            where T : Button
        {
            return (T)CurrentState.control;
        }
    }
}
