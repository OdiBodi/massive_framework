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
            this.ForEach(state => state.Control.Clicked += () => Clicked?.Invoke(state));
        }

        public T Button<T>(string id)
            where T : Button 
        {
            return (T)this.First(state => state.Id == id).Control;
        }

        public T CurrentButton<T>()
            where T : Button
        {
            return (T)CurrentState.Control;
        }
    }
}
