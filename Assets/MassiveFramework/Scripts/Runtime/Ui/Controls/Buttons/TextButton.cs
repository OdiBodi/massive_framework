using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework.Runtime
{
    public class TextButton : Button
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
