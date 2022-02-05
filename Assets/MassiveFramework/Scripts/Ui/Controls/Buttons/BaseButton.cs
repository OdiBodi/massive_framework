using System;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class BaseButton : BaseMonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup canvasGroup;

        [SerializeField]
        private Button button;

        public event Action OnClicked;

        public virtual bool Enabled
        {
            get => canvasGroup.interactable;
            set => Alpha = value ? 1f : 0.5f;
        }
        public bool Interactable
        {
            get => button.interactable;
            set
            {
                button.interactable = value;
                canvasGroup.blocksRaycasts = value;
            }
        }
        public bool Visibility
        {
            get => Alpha > 0f;
            set => Alpha = value ? 1f : 0f;
        }
        public float Alpha
        {
            get => canvasGroup.alpha;
            set
            {
                canvasGroup.alpha = value;
                Interactable = value > 0.98f;
            }
        }

        private void Awake()
        {
            SubscribeOnButton();
        }

        private void SubscribeOnButton()
        {
            button.onClick.AddListener(() => OnClicked?.Invoke());
        }
    }
}
