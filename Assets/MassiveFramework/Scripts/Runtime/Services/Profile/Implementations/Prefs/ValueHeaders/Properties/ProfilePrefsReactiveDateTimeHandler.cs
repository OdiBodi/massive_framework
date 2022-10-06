using System;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefsReactiveDateTimeHandler : ProfileReactiveValueHandler<DateTime>
    {
        protected override void Load(string id, ReactiveProperty<DateTime> property)
        {
            if (!PlayerPrefs.HasKey(id))
            {
                return;
            }
            var binaryString = PlayerPrefs.GetString(id);
            var binary = long.Parse(binaryString);
            property.Value = DateTime.FromBinary(binary);
        }

        protected override void Save(string id, ReactiveProperty<DateTime> property)
        {
            var value = property.Value.ToBinaryString();
            PlayerPrefs.SetString(id, value);
        }
    }
}
