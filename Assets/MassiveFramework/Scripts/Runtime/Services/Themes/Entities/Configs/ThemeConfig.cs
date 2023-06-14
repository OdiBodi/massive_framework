using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "theme_config", menuName = "Massive Framework/Configs/Theme Config")]
    public class ThemeConfig : ScriptableObject
    {
        [SerializeField]
        private string _id;

        [SerializeField, TableList]
        private ThemeColorResource[] _colors;

        [SerializeField, TableList]
        private ThemeSpriteResource[] _sprites;

        public string Id => _id;

        public Color Color(string id)
        {
            return _colors.FirstOrDefault(resource => resource.Id == id).Color;
        }

        public Sprite Sprite(string id)
        {
            return _sprites.FirstOrDefault(resource => resource.Id == id).Sprite;
        }
    }
}
