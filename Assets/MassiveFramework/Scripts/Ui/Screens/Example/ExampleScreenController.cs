using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class ExampleScreenController : BaseMonoBehaviour
    {
        [Inject]
        private readonly IApplicationReview applicationReview;

        [Inject]
        private readonly Pool pool;

        [SerializeField]
        private ExampleScreen view;

        private void Awake()
        {
            SubscribeOnView();
        }

        private void SubscribeOnView()
        {
            view.OnCloseButtonClicked += () =>
            {
                view.Close(ScreenClosingResult.Close);
            };
            view.OnShowAppReviewButtonClicked += () =>
            {
                applicationReview.Request();
            };
            view.OnPlayVfxButtonClicked += () =>
            {
                pool.PlayVfx("example", Vector3.zero, Quaternion.identity, Vector3.one);
            };
        }
    }
}
