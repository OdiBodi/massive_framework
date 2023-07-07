using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassiveCore.Framework.Runtime
{
    public class TapPanel : BaseMonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private bool _active = true;

        public event Action Tapped;

        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Active)
            {
                Tapped?.Invoke();
            }
        }
    }
}
