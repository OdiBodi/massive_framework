using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework.Runtime
{
    public class AnimatedNumericText : BaseMonoBehaviour
    {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private string _format = "{0}";

        [Space, SerializeField]
        private float _animationSpeed = 5f;

        private AnimatedNumber _animation;
        private ReactiveProperty<int> _number;

        public int Number
        {
            get => (int)_animation.TargetNumber;
            set => _animation.TargetNumber = value;
        }
        public ReadOnlyReactiveProperty<int> AnimatedNumber;

        private void Awake()
        {
            Initialize();
            SubscribeOnAnimation();
        }

        public void Reset(int number)
        {
            _animation.Reset(number);
        }

        private void Initialize()
        {
            InitializeAnimation();
            InitializeNumberProperties();
        }
        
        private void InitializeAnimation()
        {
            _animation = new AnimatedNumber(_animationSpeed);
            _animation.AddTo(this);
            _animation.Initialize();
        }

        private void InitializeNumberProperties()
        {
            _number = new ReactiveProperty<int>();
            AnimatedNumber = _number.ToReadOnlyReactiveProperty();
        }

        private void SubscribeOnAnimation()
        {
            _animation.Number.Subscribe(value =>
            {
                _number.Value = (int)value;
                UpdateText((int)Mathf.Round(value));
            });
        }

        private void UpdateText(int value)
        {
            _text.text = string.IsNullOrEmpty(_format) ? value.ToString() : string.Format(_format, value);
        }
    }
}
