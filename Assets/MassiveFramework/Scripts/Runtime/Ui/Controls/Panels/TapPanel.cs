using System;
using UnityEngine.EventSystems;

namespace MassiveCore.Framework
{
    public class TapPanel : BaseMonoBehaviour, IPointerDownHandler
    {
        public event Action Tapped;

        public bool Active { get; set; } = true;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Active)
            {
                Tapped?.Invoke();
            }
        }
    }
}
