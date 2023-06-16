using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class ExampleScreenController : BaseMonoBehaviour
    {
        [Inject]
        private readonly IApplicationReview _applicationReview;

        [Inject]
        private readonly IResources _resources;

        [Inject]
        private readonly IVisualEffects _visualEffects;

        [SerializeField]
        private ExampleScreen _view;

        private CurrencyResource CurrencyResource => _resources.Resource<CurrencyResource>();

        private void Start()
        {
            SubscribeOnView();
            SubscribeOnResources();
            StartAutoIncreasingNumberTimer();
        }

        private void SubscribeOnView()
        {
            _view.CloseButton.Clicked += () =>
            {
                _view.Close(ScreenClosingResult.Close);
            };
            _view.ShowAppReviewButton.Clicked += () =>
            {
                _applicationReview.Request();
            };
            _view.PlayVfxButton.Clicked += () =>
            {
                _visualEffects.PlayVisualEffect("example", Vector3.zero, Quaternion.identity, Vector3.one);
            };
            _view.IncreaseCurrencyButton.Clicked += () =>
            {
                CurrencyResource.Increase(100);
            };
            _view.SpendCurrencyButton.Clicked += () =>
            {
                CurrencyResource.Spend(100);
            };
        }

        private void SubscribeOnResources()
        {
            CurrencyResource.Amount.Subscribe(value => _view.CurrencyText.Number = value).AddTo(this);
        }

        private void StartAutoIncreasingNumberTimer()
        {
            Observable.Interval(TimeSpan.FromSeconds(2f)).Subscribe(_ => _view.AutoIncreasedNumericText.Number += 10).AddTo(this);
        }
    }
}
