#if UNITY_ANDROID

using Cysharp.Threading.Tasks;
using Google.Play.Review;
using UniRx;
using Zenject;

namespace MassiveCore.Framework
{
    public class AndroidApplicationReview : IApplicationReview
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
                return true;
            }

            var reviewManager = new ReviewManager();
            var requestResult = await reviewManager.RequestReviewFlow().OnCompleteAsObservable();
            logger.Print($"In-App Review: Request result = {requestResult.Error}");
            if (requestResult.Error != ReviewErrorCode.NoError)
            {
                return false;
            }

            var launchResult = await reviewManager.LaunchReviewFlow(requestResult.Info).OnCompleteAsObservable();
            logger.Print($"In-App Review: Launch result = {launchResult}");
            if (launchResult == ReviewErrorCode.NoError)
            {
                return false;
            }

            ApplicationReviewActive.Value = false;
            return true;
        }
    }
}

#endif // UNITY_ANDROID
