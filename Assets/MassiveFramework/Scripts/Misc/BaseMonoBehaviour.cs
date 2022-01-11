using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MassiveCore.Framework
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        public GameObject CacheGameObject
        {
            get
            {
                if (cacheGameObject == null)
                {
                    cacheGameObject = gameObject;
                }
                return cacheGameObject;
            }
        }

        public Transform CacheTransform
        {
            get
            {
                if (cacheTransform == null)
                {
                    cacheTransform = GetComponent<Transform>();
                }
                return cacheTransform;
            }
        }

        public RectTransform CacheRectTransform
        {
            get
            {
                if (cacheRectTransform == null)
                {
                    cacheRectTransform = GetComponent<RectTransform>();
                }
                return cacheRectTransform;
            }
        }

        public Image CacheImage
        {
            get
            {
                if (cacheImage == null)
                {
                    cacheImage = GetComponent<Image>();
                }
                return cacheImage;
            }
        }

        public Rigidbody CacheRigidbody
        {
            get
            {
                if (cacheRigidbody == null)
                {
                    cacheRigidbody = GetComponent<Rigidbody>();
                }
                return cacheRigidbody;
            }
        }

        private GameObject cacheGameObject;
        private Transform cacheTransform;
        private RectTransform cacheRectTransform;
        private Image cacheImage;
        private Rigidbody cacheRigidbody;

        [Inject]
        protected readonly ILogger logger;
    }
}
