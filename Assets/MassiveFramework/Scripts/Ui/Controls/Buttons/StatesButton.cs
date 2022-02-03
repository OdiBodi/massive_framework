using System;
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

        public State CurrentState
        {
            get => state;
            set
            {
                state = value;
                states.ForEach(x => x.button.Visibility = state.id == x.id);
                OnStateChanged?.Invoke(state);
            }
        }

        private void Awake()
        {
            Subscribe();
            CurrentState = states[0];
        }

        private void Subscribe()
        {
            states.ForEach(state => state.button.OnClicked += () => OnClicked?.Invoke(state));
        }
    }
}
