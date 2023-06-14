using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework.Runtime
{
    public class ThemeTextColor : ThemeComponent
    {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private string _colorId;

        protected override void OnThemeChanged(ThemeConfig config)
        {
            _text.color = config.Color(_colorId);
        }
    }
}
