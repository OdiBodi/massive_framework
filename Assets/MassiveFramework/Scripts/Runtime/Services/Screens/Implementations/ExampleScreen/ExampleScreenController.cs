using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
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
        }

        private void SubscribeOnView()
        {
            _view.OnCloseButtonClicked += () =>
            {
                _view.Close(ScreenClosingResult.Close);
            };
            _view.OnShowAppReviewButtonClicked += () =>
            {
                _applicationReview.Request();
            };
            _view.OnPlayVfxButtonClicked += () =>
            {
                _visualEffects.PlayVisualEffect("example", Vector3.zero, Quaternion.identity, Vector3.one);
            };
            _view.OnIncreaseCurrencyButtonClicked += () =>
            {
                CurrencyResource.Increase(100);
            };
            _view.OnSpendCurrencyButtonClicked += () =>
            {
                CurrencyResource.Spend(100);
            };
        }

        private void SubscribeOnResources()
        {
            CurrencyResource.Amount.Subscribe(_view.UpdateCurrency).AddTo(this);
        }
    }
}
