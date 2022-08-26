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

        private void InitGameSession()
        {
            gameSession = new GameSession(profile);
        }
    }
}
