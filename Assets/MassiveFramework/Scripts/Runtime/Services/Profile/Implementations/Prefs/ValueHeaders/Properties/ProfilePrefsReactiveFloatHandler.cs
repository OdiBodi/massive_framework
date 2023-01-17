using UniRx;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class ProfilePrefsReactiveFloatHandler : ProfileReactiveValueHandler<float>
    {
        protected override void Load(string id, ReactiveProperty<float> property)
        {
            if (!PlayerPrefs.HasKey(id))
            {
                return;
            }
            property.Value = PlayerPrefs.GetFloat(id);
        }

        protected override void Save(string id, ReactiveProperty<float> property)
        {
            var value = property.Value;
            PlayerPrefs.SetFloat(id, value);
        }
    }
}
