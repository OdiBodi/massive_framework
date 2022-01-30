using System;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ExampleScreen : Screen
    {
        [Space, SerializeField]
        private Button closeButton;
        
        [SerializeField]
        private Button showAppReviewButton;

        [SerializeField]
        private Button playVfxButton;

        public event Action OnCloseButtonClicked;
        public event Action OnShowAppReviewButtonClicked;
        public event Action OnPlayVfxButtonClicked;

        private void Awake()
        {
            SubscribeOnButtons();
        }

        private void SubscribeOnButtons()
        {
            closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            showAppReviewButton.onClick.AddListener(() => OnShowAppReviewButtonClicked?.Invoke());
            playVfxButton.onClick.AddListener(() => OnPlayVfxButtonClicked?.Invoke());
        }
    }
}
