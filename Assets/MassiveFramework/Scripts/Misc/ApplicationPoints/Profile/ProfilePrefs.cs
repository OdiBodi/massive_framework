using System;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefs : IProfile
    {
        private const string FirstLaunchDateKey = "first_launch_date";
        private const string LastSessionDateKey = "last_session_date";

        private const string NumberSessionKey = "number_session";

        private const string ApplicationReviewActiveKey = "application_review_active";

        private const string LevelIndexKey = "current_level_index";

        private bool wasLoad;

        public DateTime FirstLaunchDate { get; private set; }
        public DateTime LastSessionDate { get; set; }

        public int NumberSession { get; set; }

        public ReactiveProperty<bool> ApplicationReviewActive { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<int> LevelIndex { get; } = new ReactiveProperty<int>();

        public void Sync()
        {
            if (!wasLoad)
            {
                wasLoad = true;
                Load();
            }
            Save();
        }

        protected virtual void Load()
        {
            FirstLaunchDate = DateTime.Now.FromPlayerPrefs(FirstLaunchDateKey);
            LastSessionDate = DateTime.Now.FromPlayerPrefs(LastSessionDateKey);

            NumberSession = PlayerPrefs.GetInt(NumberSessionKey, 0);

            ApplicationReviewActive.GetPlayerPrefsBool(ApplicationReviewActiveKey, true);

            LevelIndex.GetPlayerPrefsInt(LevelIndexKey, 0);
        }

        protected virtual void Save()
        {
            FirstLaunchDate.SaveToPlayerPrefs(FirstLaunchDateKey);
            LastSessionDate.SaveToPlayerPrefs(LastSessionDateKey);

            PlayerPrefs.SetInt(NumberSessionKey, NumberSession);

            ApplicationReviewActive.SetPlayerPrefsBool(ApplicationReviewActiveKey);

            LevelIndex.SetPlayerPrefsInt(LevelIndexKey);

            PlayerPrefs.Save();
        }
    }
}
