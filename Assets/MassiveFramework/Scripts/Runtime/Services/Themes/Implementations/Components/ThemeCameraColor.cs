using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class ThemeCameraColor : ThemeComponent
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private string _colorId;

        protected override void OnThemeChanged(ThemeConfig config)
        {
            _camera.backgroundColor = config.Color(_colorId);
        }
    }
}
