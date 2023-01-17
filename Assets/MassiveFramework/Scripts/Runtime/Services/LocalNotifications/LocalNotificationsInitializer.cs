using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class LocalNotificationsInitializer : ServiceInitializer
    {
        [Inject]
        private readonly ILocalNotifications _notifications;

        [Inject]
        private readonly IConfigs _configs;

        public override UniTask<bool> Initialize()
        {
            InitializeNotifications();
            SubscribeOnApplicationEvents();
            CompleteInitialize(true);
            return base.Initialize();
        }

        private void InitializeNotifications()
        {
            _notifications.Initialize();
            _notifications.Reset();
        }

        private void SubscribeOnApplicationEvents()
        {
            Observable.EveryApplicationPause().Where(result => result).Subscribe(_ =>
            {
                var config = _configs.Config<LocalNotificationsConfig>();
                new LocalNotificationsScheduler(_notifications, _logger, config).Schedule();
            }).AddTo(this);
            Observable.EveryApplicationFocus().Where(result => result).Subscribe(_ =>
            {
                _notifications.Reset();
            }).AddTo(this);
        }
    }
}
