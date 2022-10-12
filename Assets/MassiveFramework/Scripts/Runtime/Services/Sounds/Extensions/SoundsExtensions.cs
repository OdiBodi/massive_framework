using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class SoundsExtensions
    {
        public static UniTask PlaySound(this ISounds sounds, string id, Vector3 position, Action<Sound> prepare = null)
        {
            var result = sounds.PlaySound(id, sound =>
            {
                var sound_ = sound as Sound;
                sound_.CacheTransform.position = position;
                prepare?.Invoke(sound_);
            });
            return result;
        }

        public static UniTask PlaySound(this ISounds sounds, string id, Camera camera, Vector3 position,
            Action<Sound> prepare = null)
        {
            var worldPoint = camera.ViewportToWorldPoint(position);
            return sounds.PlaySound(id, worldPoint, prepare);
        }
    }
}
