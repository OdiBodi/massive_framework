using UnityEngine;

namespace MassiveCore.Framework
{
    public class ExampleScreenController : BaseMonoBehaviour
    {
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
        }
    }
}
