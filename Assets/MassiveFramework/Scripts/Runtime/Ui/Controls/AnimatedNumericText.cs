using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class AnimatedNumericText : BaseMonoBehaviour
    {
        [SerializeField]
        private Text text;

        [SerializeField]
        private string format;

        [Space, SerializeField]
        private float animationSpeed = 5f;

        private AnimatedNumber animation;

        public int Number
        {
            get => (int)animation.TargetNumber;
            set => animation.TargetNumber = value;
        }

        private void Awake()
        {
            InitAnimation();
            SubscribeOnAnimation();
        }

        private void InitAnimation()
        {
            animation = new AnimatedNumber(animationSpeed);
            animation.AddTo(this);
            animation.Init();
        }

        private void SubscribeOnAnimation()
        {
            animation.Number.Subscribe(value => UpdateText((int)Mathf.Round(value)));
        }

        private void UpdateText(int value)
        {
            text.text = string.IsNullOrEmpty(format) ? value.ToString() : string.Format(format, value);
        }
    }
}
