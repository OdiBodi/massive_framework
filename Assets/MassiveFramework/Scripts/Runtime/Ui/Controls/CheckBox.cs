using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class CheckBox : StatesRectTransform
    {
        [SerializeField]
        private Button _button;

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
            var state = CurrentState.Id == "active" ? "inactive" : "active"; 
            ChangeState(state);
        }
    }
}
