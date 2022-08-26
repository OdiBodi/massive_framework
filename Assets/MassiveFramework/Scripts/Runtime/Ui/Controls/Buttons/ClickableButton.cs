using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MassiveCore.Framework
{
    public class ClickableButton : BaseMonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Vector2 offset = new Vector2(0f, -10f);

        [SerializeField]
        private float animationSpeed = 10f;

        private Vector3 originAnchoredPosition;

        private bool Clickable => button.interactable;

        private IEnumerator Start()
        {
            yield return null;
            originAnchoredPosition = CacheRectTransform.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Clickable)
            {
                StartOffsetAnimation(offset);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Clickable)
            {
                StartOffsetAnimation(Vector2.zero);
            }
        }

        private void StartOffsetAnimation(Vector2 offset)
        {
            var newPosition = originAnchoredPosition + (Vector3) offset;
            CacheRectTransform.DOKill();
            CacheRectTransform.DOAnchorPos(newPosition, 1f / animationSpeed).SetEase(Ease.Linear)
                .SubscribeOnComplete(this);
        }
    }
}
