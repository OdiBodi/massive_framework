using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ExampleScreen : Screen
    {
        [Space, SerializeField]
        private AnimatedNumericText _currencyText;

        [SerializeField]
        private AnimatedNumericText _animatedNumericText;

        [Space, SerializeField]
        private Button _closeButton;
        
        [SerializeField]
        private Button _showAppReviewButton;

        [SerializeField]
        private Button _playVfxButton;
        
        [SerializeField]
        private Button _increaseCurrencyButton;

        [SerializeField]
        private Button _spendCurrencyButton;

        public event Action OnCloseButtonClicked;
        public event Action OnShowAppReviewButtonClicked;
        public event Action OnPlayVfxButtonClicked;
        public event Action OnIncreaseCurrencyButtonClicked;
        public event Action OnSpendCurrencyButtonClicked;

        private void Start()
        {
            SubscribeOnButtons();
            StartIncreasingNumber();
        }

        public void UpdateCurrency(int amount)
        {
            _currencyText.Number = amount;
        }

        private void SubscribeOnButtons()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            _showAppReviewButton.onClick.AddListener(() => OnShowAppReviewButtonClicked?.Invoke());
            _increaseCurrencyButton.onClick.AddListener(() => OnIncreaseCurrencyButtonClicked?.Invoke());
            _spendCurrencyButton.onClick.AddListener(() => OnSpendCurrencyButtonClicked?.Invoke());
        }

        private void StartIncreasingNumber()
        {
            Observable.Interval(TimeSpan.FromSeconds(2f)).Subscribe(_ => _animatedNumericText.Number += 10).AddTo(this);
        }
    }
}
