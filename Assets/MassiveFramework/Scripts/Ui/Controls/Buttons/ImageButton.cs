using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ImageButton : BaseButton
    {
        [SerializeField]
        private Image image;

        public Sprite Sprite
        {
            get => image.sprite;
            set => image.sprite = value;
        }
    }
}
