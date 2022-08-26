using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class EditorApplicationReview : IApplicationReview
    {
        [Inject]
        private readonly ILogger logger;

        [Inject]
        private readonly ICustomProfile profile;

        public async UniTask<bool> Request()
        {
            if (!profile.ApplicationReviewActive.Value)
            {
                return false;
            }
            logger.Print("ApplicationReview::Request()");
            profile.ApplicationReviewActive.Value = false;
            return true;
        }
    }
}
