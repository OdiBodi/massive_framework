using System;
using UniRx;

namespace MassiveCore.Framework
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

        private ReactiveProperty<int> SessionNumberProperty => _profile.Property<int>(ProfileIds.SessionNumber);
        private ReactiveProperty<DateTime> LastSessionDateProperty => _profile.Property<DateTime>(ProfileIds.LastSessionDate);

        public void Initialize()
        {
            IncreaseSession();
            Observable.EveryApplicationPause().Where(result => result).Subscribe(_ =>
            { 
                UpdateLastSessionDate();
                _profile.Synchronize();
            }).AddTo(_initializer);
            Observable.EveryApplicationFocus().Where(result => result).Subscribe(_ =>
            {
                UpdateSession();
            }).AddTo(_initializer);
            Observable.OnceApplicationQuit().Subscribe(_ =>
            {
                UpdateLastSessionDate();
                _profile.Synchronize();
            }).AddTo(_initializer);
        }

        private void IncreaseSession()
        {
            SessionNumberProperty.Value++;
            UpdateLastSessionDate();
        }

        private void UpdateSession()
        {
            var time = (DateTime.Now - LastSessionDateProperty.Value).TotalMinutes;
            if (time > 30)
            {
                SessionNumberProperty.Value++;
            }
            UpdateLastSessionDate();
        }

        private void UpdateLastSessionDate()
        {
            LastSessionDateProperty.Value = DateTime.Now;
        }
    }
}
