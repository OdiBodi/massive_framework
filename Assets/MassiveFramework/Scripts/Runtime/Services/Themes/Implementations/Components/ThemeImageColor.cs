using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework.Runtime
{
    public class ThemeImageColor : ThemeComponent
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private string _colorId;

        protected override void OnThemeChanged(ThemeConfig config)
        {
            _image.color = config.Color(_colorId);
        }
    }
}
