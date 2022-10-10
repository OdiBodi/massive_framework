using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class VisualEffectsExtensions
    {
        public static UniTask PlayVisualEffect(this IVisualEffects visualEffects, string id, Vector3 position,
            Quaternion rotation, Vector3 scale, Action<VisualEffect> prepare = null)
        {
            var result = visualEffects.PlayVisualEffect(id, effect =>
            {
                var visualEffect = effect as VisualEffect;
                visualEffect.CacheTransform.position = position;
                visualEffect.CacheTransform.rotation = rotation;
                visualEffect.CacheTransform.localScale = scale;
                prepare?.Invoke(visualEffect);
            });
            return result;
        }

        public static UniTask PlayVisualEffect(this IVisualEffects visualEffects, string id, Camera camera,
            Vector3 position, Quaternion rotation, Vector3 scale, Action<VisualEffect> prepare = null)
        {
            var worldPoint = camera.ViewportToWorldPoint(position);
            return visualEffects.PlayVisualEffect(id, worldPoint, rotation, scale, prepare);
        }
    }
}
