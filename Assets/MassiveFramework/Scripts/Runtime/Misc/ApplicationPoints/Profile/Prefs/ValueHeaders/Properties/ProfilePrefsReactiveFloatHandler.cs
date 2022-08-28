using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefsReactiveFloatHandler : ProfileReactiveValueHandler<float>
    {
        protected override void Load(string id, ReactiveProperty<float> property)
        {
            property.Value = PlayerPrefs.GetFloat(id);
        }

        protected override void Save(string id, ReactiveProperty<float> property)
        {
            var value = property.Value;
            PlayerPrefs.SetFloat(id, value);
        }
    }
}
