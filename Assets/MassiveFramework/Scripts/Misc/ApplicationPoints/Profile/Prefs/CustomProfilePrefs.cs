using UniRx;

namespace MassiveCore.Framework
{
    public class CustomProfilePrefs : ProfilePrefs, ICustomProfile
    {
        private const string ApplicationReviewActiveKey = "application_review_active";

        private const string LevelIndexKey = "level_index";

        public ReactiveProperty<bool> ApplicationReviewActive { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<int> LevelIndex { get; } = new ReactiveProperty<int>();

        protected override void Load()
        {
            base.Load();

            ApplicationReviewActive.GetPlayerPrefsBool(ApplicationReviewActiveKey, true);

            LevelIndex.GetPlayerPrefsInt(LevelIndexKey, 0);
        }

        protected override void Save()
        {
            ApplicationReviewActive.SetPlayerPrefsBool(ApplicationReviewActiveKey);

            LevelIndex.SetPlayerPrefsInt(LevelIndexKey);

            base.Save();
        }
    }
}
