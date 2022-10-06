using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class ExampleScreenController : BaseMonoBehaviour
    {
        [Inject]
        private readonly IApplicationReview _applicationReview;

        [Inject]
        private readonly IVisualEffects _visualEffects;

        [SerializeField]
        private ExampleScreen _view;

        private void Awake()
        {
            SubscribeOnView();
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
        }
    }
}
