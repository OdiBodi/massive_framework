using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class ExampleScreenController : BaseMonoBehaviour
    {
        [Inject]
        private readonly IApplicationReview applicationReview;
        
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
                view.Close(ClosingResult.Close);
            };
            view.OnShowAppReviewButtonClicked += () =>
            {
                applicationReview.Request();
            };
        }
    }
}
