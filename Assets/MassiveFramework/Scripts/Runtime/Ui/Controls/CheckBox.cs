using UnityEngine;

namespace MassiveCore.Framework
{
    public class CheckBox : StatesRectTransform
    {
        [SerializeField]
        private BaseButton _button;

        protected override void Awake()
        {
            Subscribe();
            base.Awake();
        }

        private void Subscribe()
        {
            _button.Clicked += OnClicked;
        }

        private void OnClicked()
        {
            var state = CurrentState.id == "active" ? "inactive" : "active"; 
            ChangeState(state);
        }
    }
}
