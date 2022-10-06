using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class TextButton : BaseButton
    {
        [SerializeField]
        private Text _text;

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }
    }
}
