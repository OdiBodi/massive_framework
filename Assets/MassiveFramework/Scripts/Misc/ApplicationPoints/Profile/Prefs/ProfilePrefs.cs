using System;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefs : IProfile
    {
        private const string FirstLaunchDateKey = "first_launch_date";
        private const string LastSessionDateKey = "last_session_date";

        private const string NumberSessionKey = "number_session";

        private bool wasLoad;

        public DateTime FirstLaunchDate { get; private set; }
        public DateTime LastSessionDate { get; set; }

        public int NumberSession { get; set; }

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
        }

        protected virtual void Save()
        {
            FirstLaunchDate.SaveToPlayerPrefs(FirstLaunchDateKey);
            LastSessionDate.SaveToPlayerPrefs(LastSessionDateKey);

            PlayerPrefs.SetInt(NumberSessionKey, NumberSession);

            PlayerPrefs.Save();
        }
    }
}
