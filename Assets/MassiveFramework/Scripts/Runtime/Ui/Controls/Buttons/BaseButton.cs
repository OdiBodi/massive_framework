using System;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class BaseButton : BaseMonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup _canvasGroup;

        [SerializeField]
        private Button _button;

        public event Action OnClicked;

        public virtual bool Enabled
        {
            get => _canvasGroup.interactable;
            set => Alpha = value ? 1f : 0.5f;
        }
        public bool Interactable
        {
            get => _button.interactable;
            set
            {
                _button.interactable = value;
                _canvasGroup.blocksRaycasts = value;
            }
        }
        public bool Visibility
        {
            get => Alpha > 0f;
            set => Alpha = value ? 1f : 0f;
        }
        public float Alpha
        {
            get => _canvasGroup.alpha;
            set
            {
                _canvasGroup.alpha = value;
                Interactable = value > 0.98f;
            }
        }

        private void Awake()
        {
            SubscribeOnButton();
        }

        private void SubscribeOnButton()
        {
            _button.onClick.AddListener(() => OnClicked?.Invoke());
        }
    }
}
