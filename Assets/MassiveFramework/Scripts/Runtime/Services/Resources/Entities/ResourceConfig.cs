using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "resource_config", menuName = "Massive Framework/Configs/Resource Config")]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField]
        private string _id;

        [SerializeField]
        private int _amount;

        [SerializeField]
        private Sprite _icon;

        public string Id => _id;
        public int Amount => _amount;
        public Sprite Icon => _icon;
    }
}
