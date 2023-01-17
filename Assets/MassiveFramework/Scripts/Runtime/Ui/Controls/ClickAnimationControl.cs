using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassiveCore.Framework.Runtime
{
    public class ClickAnimationControl : BaseMonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Vector2 _offset = new(0f, -10f);

        [SerializeField]
        private float _animationSpeed = 10f;

        private Vector2 _originAnchoredPosition;

        private IEnumerator Start()
        {
            yield return null;
            _originAnchoredPosition = CacheRectTransform.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartOffsetAnimation(_offset);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StartOffsetAnimation(Vector2.zero);
        }

        private void StartOffsetAnimation(Vector2 offset)
        {
            var position = _originAnchoredPosition + offset;
            var duration = 1f / _animationSpeed;
            CacheRectTransform.DOKill();
            CacheRectTransform.DOAnchorPos(position, duration).SetEase(Ease.Linear).SubscribeOnComplete(this);
        }
    }
}
