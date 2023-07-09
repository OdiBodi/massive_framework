using System;
using UniRx;

namespace MassiveCore.Framework.Runtime
{
    public class Session
    {
        private readonly IProfile _profile;
        private readonly SessionInitializer _initializer;

        public Session(IProfile profile, SessionInitializer initializer)
        {
            _profile = profile;
            _initializer = initializer;
        }

        private ReactiveProperty<int> SessionNumber => _profile.Property<int>(ProfileIds.SessionNumber);
        private ReactiveProperty<DateTime> LastSessionDate => _profile.Property<DateTime>(ProfileIds.LastSessionDate);

        public void Initialize()
        {
            IncreaseSession();
            Observable.EveryApplicationPause().Where(result => result).Subscribe(_ => OnApplicationInactive()).AddTo(_initializer);
            Observable.EveryApplicationFocus().Subscribe(result =>
            {
                if (result)
                {
                    UpdateSession();
                }
                else
                {
                    OnApplicationInactive();
                }
            }).AddTo(_initializer);
            Observable.OnceApplicationQuit().Subscribe(_ => OnApplicationInactive()).AddTo(_initializer);
        }

        private void IncreaseSession()
        {
            SessionNumber.Value++;
            UpdateLastSessionDate();
        }

        private void UpdateSession()
        {
            var time = (DateTime.Now - LastSessionDate.Value).TotalMinutes;
            if (time > 30)
            {
                SessionNumber.Value++;
            }
            UpdateLastSessionDate();
        }

        private void UpdateLastSessionDate()
        {
            LastSessionDate.Value = DateTime.Now;
        }

        private void OnApplicationInactive()
        {
            UpdateLastSessionDate();
            _profile.Synchronize();
        }
    }
}
