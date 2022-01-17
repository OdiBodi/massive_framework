using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class TextButton : BaseButton
    {
        [SerializeField]
        private Text text;

        public string Text
        {
            get => text.text;
            set => text.text = value;
        }
    }
}
