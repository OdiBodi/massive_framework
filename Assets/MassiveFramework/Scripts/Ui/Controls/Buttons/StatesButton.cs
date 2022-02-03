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

        public State CurrentState
        {
            get => state;
            set
            {
                state = value;
                states.ForEach(x => x.button.Visibility = state.id == x.id);
            }
        }

        private void Awake()
        {
            CurrentState = states[0];
        }
    }
}
