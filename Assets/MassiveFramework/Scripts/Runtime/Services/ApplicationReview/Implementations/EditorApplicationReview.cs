using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorApplicationReview : IApplicationReview
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IProfile _profile;

        private ReactiveProperty<bool> ApplicationReviewActiveProperty =>
            _profile.Property<bool>(ProfileIds.ApplicationReviewActive);

        public async UniTask<bool> Request()
        {
            if (!ApplicationReviewActiveProperty.Value)
            {
                return false;
            }
            _logger.Print("ApplicationReview::Request()");
            ApplicationReviewActiveProperty.Value = false;
            return true;
        }
    }
}
