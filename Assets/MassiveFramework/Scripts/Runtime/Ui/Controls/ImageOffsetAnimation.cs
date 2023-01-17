using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MassiveCore.Framework.Runtime
{
    [RequireComponent(typeof(Image))]
    public class ImageOffsetAnimation : BaseMonoBehaviour
    {
        [SerializeField]
        private Vector2 direction = Vector2.one;

        [SerializeField]
        private float speed = 5f;

        private void Awake()
        {
            var material = CacheImage.material;
            var duration = 1f / speed;
            CacheImage.material = new Material(material);
            CacheImage.material.DOOffset(direction, duration).SetEase(Ease.Linear).SetLoops(-1);
        }
    }
}
