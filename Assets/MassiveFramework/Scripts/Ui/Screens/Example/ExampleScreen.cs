using System;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ExampleScreen : Screen
    {
        [Space, SerializeField]
        private Button button;

        public event Action OnCloseButtonClicked;

        private void Awake()
        {
            SubscribeOnButtons();
        }

        private void SubscribeOnButtons()
        {
            button.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
        }
    }
}
