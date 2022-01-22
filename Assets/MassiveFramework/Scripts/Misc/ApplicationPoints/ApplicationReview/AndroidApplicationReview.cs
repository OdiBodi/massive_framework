#if UNITY_ANDROID

using System.Threading.Tasks;
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
        private readonly ICustomProfile profile;

        public async Task<bool> Request()
        {
            if (profile.ApplicationReviewActive.Value)
            {
                var reviewManager = new ReviewManager();
                var requestResult = await reviewManager.RequestReviewFlow().OnCompleteAsObservable();
                logger.Print($"In-App Review: Request result = {requestResult.Error}");
                if (requestResult.Error == ReviewErrorCode.NoError)
                {
                    var launchResult = await reviewManager.LaunchReviewFlow(requestResult.Info).OnCompleteAsObservable();
                    logger.Print($"In-App Review: Launch result = {launchResult}");
                    if (launchResult == ReviewErrorCode.NoError)
                    {
                        profile.ApplicationReviewActive.Value = false;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

#endif // UNITY_ANDROID
