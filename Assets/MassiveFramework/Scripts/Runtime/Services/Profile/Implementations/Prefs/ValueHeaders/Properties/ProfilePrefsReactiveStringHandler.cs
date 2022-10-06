using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefsReactiveStringHandler : ProfileReactiveValueHandler<string>
    {
        protected override void Load(string id, ReactiveProperty<string> property)
        {
            if (!PlayerPrefs.HasKey(id))
            {
                return;
            }
            property.Value = PlayerPrefs.GetString(id);
        }

        protected override void Save(string id, ReactiveProperty<string> property)
        {
            var value = property.Value;
            PlayerPrefs.SetString(id, value);
        }
    }
}
