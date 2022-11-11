using I2.Loc;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "localized_local_notification_config", menuName = "Massive Framework/Configs/Localized Local Notification Config")]
    public class LocalizedLocalNotificationConfig : LocalNotificationConfig
    {
        public override string Title => LocalizationManager.GetTranslation(base.Title);
        public override string Text => LocalizationManager.GetTranslation(base.Text);
    }
}
