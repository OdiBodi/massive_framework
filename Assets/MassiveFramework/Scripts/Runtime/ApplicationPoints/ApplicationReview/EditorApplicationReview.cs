using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorApplicationReview : IApplicationReview
    {
        [Inject]
        private readonly ILogger logger;

        [Inject]
        private readonly IProfile profile;

        private ReactiveProperty<bool> ApplicationReviewActive => profile.Property<bool>(ProfileIds.ApplicationReviewActive);

        public async UniTask<bool> Request()
        {
            if (!ApplicationReviewActive.Value)
            {
                return false;
            }
            logger.Print("ApplicationReview::Request()");
            ApplicationReviewActive.Value = false;
            return true;
        }
    }
}
