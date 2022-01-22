using UniRx;

namespace MassiveCore.Framework
{
    public interface ICustomProfile : IProfile
    {
        ReactiveProperty<bool> ApplicationReviewActive { get; }

        ReactiveProperty<int> LevelIndex { get; }
    }
}
