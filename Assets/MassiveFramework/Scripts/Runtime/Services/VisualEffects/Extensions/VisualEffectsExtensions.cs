using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class VisualEffectsExtensions
    {
        public static UniTask PlayVisualEffect(this IVisualEffects visualEffects, string id, Vector3 position,
            Quaternion rotation, Vector3 scale, Action<VisualEffect> onPrepare = null)
        {
            var visualEffect = visualEffects.VisualEffect(id) as VisualEffect;
            visualEffect.CacheTransform.position = position;
            visualEffect.CacheTransform.rotation = rotation;
            visualEffect.CacheTransform.localScale = scale;
            onPrepare?.Invoke(visualEffect);
            return visualEffect.Play();
        }

        public static UniTask PlayVisualEffect(this IVisualEffects visualEffects, string id, Camera camera,
            Vector3 position, Quaternion rotation, Vector3 scale, Action<VisualEffect> onPrepare = null)
        {
            var worldPoint = camera.ViewportToWorldPoint(position);
            return visualEffects.PlayVisualEffect(id, worldPoint, rotation, scale, onPrepare);
        }
    }
}
