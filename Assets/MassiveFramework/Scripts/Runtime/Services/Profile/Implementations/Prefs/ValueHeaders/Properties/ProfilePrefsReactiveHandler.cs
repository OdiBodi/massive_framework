using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefsReactiveHandler<T> : ProfileReactiveValueHandler<T>
    {
        protected override void Load(string id, ReactiveProperty<T> property)
        {
            if (!PlayerPrefs.HasKey(id))
            {
                return;
            }
            var json = PlayerPrefs.GetString(id, "{}");
            property.DeserializeFromJson(json);
        }

        protected override void Save(string id, ReactiveProperty<T> property)
        {
            var json = property.SerializeToJson();
            PlayerPrefs.SetString(id, json);
        }
    }
}
