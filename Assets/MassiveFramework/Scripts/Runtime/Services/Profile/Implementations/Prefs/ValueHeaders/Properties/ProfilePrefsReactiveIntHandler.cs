using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefsReactiveIntHandler : ProfileReactiveValueHandler<int>
    {
        protected override void Load(string id, ReactiveProperty<int> property)
        {
            if (!PlayerPrefs.HasKey(id))
            {
                return;
            }
            property.Value = PlayerPrefs.GetInt(id);
        }

        protected override void Save(string id, ReactiveProperty<int> property)
        {
            var value = property.Value;
            PlayerPrefs.SetInt(id, value);
        }
    }
}
