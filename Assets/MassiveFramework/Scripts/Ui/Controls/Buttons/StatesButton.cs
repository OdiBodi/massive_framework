using System;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class StatesButton : BaseMonoBehaviour
    {
        [Serializable]
        public struct State
        {
            public string id;
            public BaseButton button;
        }

        [SerializeField]
        private State[] states;

        private State state;

        public event Action<State> OnClicked;
        public event Action<State> OnStateChanged;

        public State CurrentState => state;

        private void Awake()
        {
            Subscribe();
            UpdateState(states[0].id);
        }

        private void Subscribe()
        {
            states.ForEach(state => state.button.OnClicked += () => OnClicked?.Invoke(state));
        }

        public T Button<T>(string id) where T : BaseButton 
        {
            return (T)states.First(state => state.id == id).button;
        }

        public T CurrentButton<T>() where T : BaseButton
        {
            return (T)state.button;
        }

        public void UpdateState(string id)
        {
            if (state.id == id)
            {
                return;
            }
            state = states.First(x => x.id == id);
            states.ForEach(x => x.button.UpdateActivity(state.id == x.id));
            OnStateChanged?.Invoke(state);
        }
    }
}
