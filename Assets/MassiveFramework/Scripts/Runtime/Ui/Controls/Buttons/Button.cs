using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassiveCore.Framework.Runtime
{
    public class Button : BaseMonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private bool _interactable = true;

        public event Action Clicked;

        public bool Interactable
        {
            get => _interactable;
            set => _interactable = value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable)
            {
                return;
            }
            Clicked?.Invoke();
        }
    }
}
