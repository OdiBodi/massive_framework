using System;
using Zenject;

namespace MassiveCore.Framework
{
    public class ProfilePoint : ApplicationPoint
    {
        [Inject]
        private readonly IProfile profile;

        private GameSession gameSession;

        public override void Init()
        {
            InitProfileValues();
            profile.Sync();

            InitGameSession();
            gameSession.Increase();

            Complete();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (profile == null)
            {
                return;
            }
            if (pauseStatus)
            {
                profile.Sync();
                gameSession.UpdateLastDate();
            }
            else
            {
                gameSession.Update();
            }
        }

        private void OnApplicationQuit()
        {
            if (profile == null)
            {
                return;
            }
            profile.Sync();
            gameSession.UpdateLastDate();
        }

        private void InitProfileValues()
        {
            var nowTime = DateTime.Now;

            profile.Property(ProfileIds.FirstLaunchDate, nowTime);

            profile.Property(ProfileIds.SessionNumber, -1);
            profile.Property(ProfileIds.LastSessionDate, nowTime);

            profile.Property(ProfileIds.ApplicationReviewActive, true);

            profile.Property(ProfileIds.LevelIndex, 0);
        }

        private void InitGameSession()
        {
            gameSession = new GameSession(profile);
        }
    }
}
