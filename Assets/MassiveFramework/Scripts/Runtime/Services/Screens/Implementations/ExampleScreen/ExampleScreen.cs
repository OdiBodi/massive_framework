using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework.Runtime
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

        public event Action CloseButtonClicked;
        public event Action ShowAppReviewButtonClicked;
        public event Action PlayVfxButtonClicked;
        public event Action IncreaseCurrencyButtonClicked;
        public event Action SpendCurrencyButtonClicked;

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
            _closeButton.onClick.AddListener(() => CloseButtonClicked?.Invoke());
            _showAppReviewButton.onClick.AddListener(() => ShowAppReviewButtonClicked?.Invoke());
            _playVfxButton.onClick.AddListener(() => PlayVfxButtonClicked?.Invoke());
            _increaseCurrencyButton.onClick.AddListener(() => IncreaseCurrencyButtonClicked?.Invoke());
            _spendCurrencyButton.onClick.AddListener(() => SpendCurrencyButtonClicked?.Invoke());
        }

        private void StartIncreasingNumber()
        {
            Observable.Interval(TimeSpan.FromSeconds(2f)).Subscribe(_ => _animatedNumericText.Number += 10).AddTo(this);
        }
    }
}
