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

        public State this[string id] => states.FirstOrDefault(state => state.id == id);
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

        public void UpdateState(string id)
        {
            if (CurrentState.id == id)
            {
                return;
            }
            state = states.First(x => x.id == id);
            states.ForEach(x => x.button.Visibility = state.id == x.id);
            OnStateChanged?.Invoke(state);
        }
    }
}
