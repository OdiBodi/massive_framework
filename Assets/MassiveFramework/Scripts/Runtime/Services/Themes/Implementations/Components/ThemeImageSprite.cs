using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework.Runtime
{
    public class ThemeImageSprite : ThemeComponent
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private string _spriteId;

        protected override void OnThemeChanged(ThemeConfig config)
        {
            _image.sprite = config.Sprite(_spriteId);
        }
    }
}
