#if UNITY_ANDROID

using UniRx;
using System;
using Google.Play.Common;
using Google.Play.Review;

namespace MassiveCore.Framework.Runtime
{
    public static class PlayAsyncOperationExtensions
    {
        public static IObservable<(PlayReviewInfo Info, ReviewErrorCode Error)> OnCompleteAsObservable(
            this PlayAsyncOperation<PlayReviewInfo, ReviewErrorCode> asyncOperation)
        {
            return Observable.Create<(PlayReviewInfo, ReviewErrorCode)>(o =>
            {
                asyncOperation.Completed += operation =>
                {
                    o.OnNext((operation.GetResult(), operation.Error));
                    o.OnCompleted();
                };
                return Disposable.Empty;
            });
        }

        public static IObservable<ReviewErrorCode> OnCompleteAsObservable(this PlayAsyncOperation<VoidResult,
            ReviewErrorCode> asyncOperation)
        {
            return Observable.Create<ReviewErrorCode>(o =>
            {
                asyncOperation.Completed += operation =>
                {
                    o.OnNext(operation.Error);
                    o.OnCompleted();
                };
                return Disposable.Empty;
            });
        }
    }
}

#endif // UNITY_ANDROID
