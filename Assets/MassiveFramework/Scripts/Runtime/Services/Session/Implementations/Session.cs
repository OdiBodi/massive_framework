using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class Session : BaseMonoBehaviour
    {
        [Inject]
        private readonly IProfile _profile;

        [SerializeField]
        private SessionInitializer _initializer;

        private ReactiveProperty<int> SessionNumber => _profile.Property<int>(ProfileIds.SessionNumber);
        private ReactiveProperty<DateTime> LastSessionDate => _profile.Property<DateTime>(ProfileIds.LastSessionDate);

        private void Start()
        {
            SubscribeOnInitializer();
        }

        private void SubscribeOnInitializer()
        {
            _initializer.Initialized.Where(result => result).Subscribe(_ =>
            {
                IncreaseSession();
                Observable.EveryApplicationPause().Where(result => result).Subscribe(_ =>
                {
                    UpdateLastSessionDate();
                    _profile.Synchronize();
                }).AddTo(this);
                Observable.EveryApplicationFocus().Where(result => result).Subscribe(_ =>
                {
                    UpdateSession();
                }).AddTo(this);
                Observable.OnceApplicationQuit().Subscribe(_ =>
                {
                    UpdateLastSessionDate();
                    _profile.Synchronize();
                }).AddTo(this);
            }).AddTo(this);
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
    }
}
