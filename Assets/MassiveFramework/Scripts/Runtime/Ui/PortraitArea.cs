using UniRx;
using UnityEngine;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class PortraitArea : BaseMonoBehaviour
    {
        [Inject]
        private readonly IScreenResolution _screenResolution;

        [SerializeField]
        private int _referenceWidth = 1125;

        private Canvas _canvas;

        private Vector2 _originAnchorMin;
        private Vector2 _originAnchorMax;

        private void Awake()
        {
            CacheCanvas();
            CacheOriginAnchors();
            SubscribeOnScreenResolution();
        }

        private void CacheCanvas()
        {
            _canvas = GetComponentInParent<Canvas>();
        }
        
        private void CacheOriginAnchors()
        {
            _originAnchorMin = CacheRectTransform.anchorMin;
            _originAnchorMax = CacheRectTransform.anchorMax;
        }

        private void SubscribeOnScreenResolution()
        {
            _screenResolution.Resolution.Subscribe(UpdateWidth).AddTo(this);
        }

        private void UpdateWidth(Resolution resolution)
        {
            Vector2 anchorMin;
            Vector2 anchorMax;

            var orientation = new ScreenOrientation(resolution).Orientation;
            if (orientation == Orientation.Portrait)
            {
                anchorMin = _originAnchorMin;
                anchorMax = _originAnchorMax;
            }
            else
            {
                anchorMin = CacheRectTransform.anchorMin;
                anchorMax = CacheRectTransform.anchorMax;

                var halfFactor = (float)_referenceWidth / resolution.width * _canvas.scaleFactor / 2f;
                anchorMin.x = 0.5f - halfFactor;
                anchorMax.x = 0.5f + halfFactor;
            }

            CacheRectTransform.anchorMin = anchorMin;
            CacheRectTransform.anchorMax = anchorMax;
        }
    }
}
