using UniRx;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class ProfilePrefsReactiveBoolHandler : ProfileReactiveValueHandler<bool>
    {
        protected override void Load(string id, ReactiveProperty<bool> property)
        {
            if (!PlayerPrefs.HasKey(id))
            {
                return;
            }
            property.Value = PlayerPrefs.GetInt(id) == 1;
        }

        protected override void Save(string id, ReactiveProperty<bool> property)
        {
            var value = property.Value ? 1 : 0;
            PlayerPrefs.SetInt(id, value);
        }
    }
}
