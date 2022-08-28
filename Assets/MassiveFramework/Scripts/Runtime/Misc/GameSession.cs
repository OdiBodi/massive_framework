using System;
using UniRx;

namespace MassiveCore.Framework
{
    public class GameSession
    {
        private readonly IProfile profile;

        public GameSession(IProfile profile)
        {
            this.profile = profile;
        }

        private ReactiveProperty<int> SessionNumber => profile.Property<int>(ProfileIds.SessionNumber);
        private ReactiveProperty<DateTime> LastSessionDate => profile.Property<DateTime>(ProfileIds.LastSessionDate);

        public void Increase()
        {
            SessionNumber.Value++;
            UpdateLastDate();
        }

        public void Update()
        {
            var time = (DateTime.Now - LastSessionDate.Value).TotalMinutes;
            if (time > 30)
            {
                SessionNumber.Value++;
            }
            UpdateLastDate();
        }

        public void UpdateLastDate()
        {
            LastSessionDate.Value = DateTime.Now;
        }
    }
}
