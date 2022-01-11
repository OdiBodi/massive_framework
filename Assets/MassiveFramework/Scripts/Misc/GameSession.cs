using System;

namespace MassiveCore.Framework
{
    public class GameSession
    {
        private IProfile profile;

        public GameSession(IProfile profile)
        {
            this.profile = profile;
        }

        public void Increase()
        {
            ++profile.NumberSession;
            UpdateLastDate();
        }

        public void Update()
        {
            var time = (DateTime.Now - profile.LastSessionDate).TotalMinutes;
            if (time > 30)
            {
                ++profile.NumberSession;
            }
            UpdateLastDate();
        }

        public void UpdateLastDate()
        {
            profile.LastSessionDate = DateTime.Now;
        }
    }
}
