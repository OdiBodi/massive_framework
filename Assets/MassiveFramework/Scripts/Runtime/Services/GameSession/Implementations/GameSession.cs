using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework
{
    public class GameSession : BaseMonoBehaviour
    {
        [Inject]
        private readonly IProfile profile;

        [SerializeField]
        private GameSessionInitializer initializer;

        private ReactiveProperty<int> SessionNumber => profile.Property<int>(ProfileIds.SessionNumber);
        private ReactiveProperty<DateTime> LastSessionDate => profile.Property<DateTime>(ProfileIds.LastSessionDate);

        private void Start()
        {
            SubscribeOnInitializer();
        }

        private void SubscribeOnInitializer()
        {
            initializer.Initialized.Subscribe(_ => {}, () =>
            {
                IncreaseSession();
                Observable.EveryApplicationPause().Subscribe(_ =>
                {
                    UpdateLastSessionDate();
                    profile.Sync();
                }).AddTo(this);
                Observable.EveryApplicationFocus().Subscribe(_ =>
                {
                    UpdateSession();
                }).AddTo(this);
                Observable.OnceApplicationQuit().Subscribe(_ =>
                {
                    UpdateLastSessionDate();
                    profile.Sync();
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
