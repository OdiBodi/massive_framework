using System;
using UnityEngine.EventSystems;

namespace MassiveCore.Framework
{
    public class TapPanel : BaseMonoBehaviour, IPointerDownHandler
    {
        public event Action OnTapped;

        public bool Active { get; set; } = true;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Active)
            {
                OnTapped?.Invoke();
            }
        }
    }
}
