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

        private ReactiveProperty<bool> ApplicationReviewActive => _profile.Property<bool>(ProfileIds.ApplicationReviewActive);

        public async UniTask<bool> Request()
        {
            if (!ApplicationReviewActive.Value)
            {
                return false;
            }
            _logger.Print("ApplicationReview::Request()");
            ApplicationReviewActive.Value = false;
            return true;
        }
    }
}
