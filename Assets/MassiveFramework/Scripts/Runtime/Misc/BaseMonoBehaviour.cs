using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MassiveCore.Framework
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        [Inject]
        protected readonly ILogger _logger;

        private GameObject _cacheGameObject;
        private Transform _cacheTransform;
        private RectTransform _cacheRectTransform;
        private Image _cacheImage;
        private Rigidbody _cacheRigidbody;

        public GameObject CacheGameObject
        {
            get
            {
                if (_cacheGameObject == null)
                {
                    _cacheGameObject = gameObject;
                }
                return _cacheGameObject;
            }
        }

        public Transform CacheTransform
        {
            get
            {
                if (_cacheTransform == null)
                {
                    _cacheTransform = GetComponent<Transform>();
                }
                return _cacheTransform;
            }
        }

        public RectTransform CacheRectTransform
        {
            get
            {
                if (_cacheRectTransform == null)
                {
                    _cacheRectTransform = GetComponent<RectTransform>();
                }
                return _cacheRectTransform;
            }
        }

        public Image CacheImage
        {
            get
            {
                if (_cacheImage == null)
                {
                    _cacheImage = GetComponent<Image>();
                }
                return _cacheImage;
            }
        }

        public Rigidbody CacheRigidbody
        {
            get
            {
                if (_cacheRigidbody == null)
                {
                    _cacheRigidbody = GetComponent<Rigidbody>();
                }
                return _cacheRigidbody;
            }
        }
    }
}
