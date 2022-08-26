using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ExampleScreen : Screen
    {
        [Space, SerializeField]
        private AnimatedNumericText animatedNumericText;

        [SerializeField]
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
            StartIncreasingNumber();
        }

        private void SubscribeOnButtons()
        {
            closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            showAppReviewButton.onClick.AddListener(() => OnShowAppReviewButtonClicked?.Invoke());
            playVfxButton.onClick.AddListener(() => OnPlayVfxButtonClicked?.Invoke());
        }

        private void StartIncreasingNumber()
        {
            Observable.Interval(TimeSpan.FromSeconds(2f)).Subscribe(_ => animatedNumericText.Number += 10)
                .AddTo(this);
        }
    }
}
