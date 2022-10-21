using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class ProfilePrefsReactiveCollectionHandler<T> : ProfileReactiveCollectionHandler<T>
    {
        protected override void Load(string id, ReactiveCollection<T> collection)
        {
            if (!PlayerPrefs.HasKey(id))
            {
                return;
            }
            var json = PlayerPrefs.GetString(id, "[]");
            collection.DeserializeFromJson(json);
        }

        protected override void Save(string id, ReactiveCollection<T> collection)
        {
            var json = collection.SerializeToJson();
            PlayerPrefs.SetString(id, json);
        }
    }
}
