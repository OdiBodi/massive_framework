using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ExampleScreen : Screen
    {
        [Space, SerializeField]
        private AnimatedNumericText _animatedNumericText;

        [SerializeField]
        private Button _closeButton;
        
        [SerializeField]
        private Button _showAppReviewButton;

        [SerializeField]
        private Button _playVfxButton;

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
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            _showAppReviewButton.onClick.AddListener(() => OnShowAppReviewButtonClicked?.Invoke());
            _playVfxButton.onClick.AddListener(() => OnPlayVfxButtonClicked?.Invoke());
        }

        private void StartIncreasingNumber()
        {
            Observable.Interval(TimeSpan.FromSeconds(2f)).Subscribe(_ => _animatedNumericText.Number += 10).AddTo(this);
        }
    }
}
