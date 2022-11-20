using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public static class SoundsExtensions
    {
        public static UniTask PlaySoundByEnable(this ISounds sounds, string id, IProfile profile, Action<ISound> prepare = null)
        {
            var enabled = profile.Property<bool>(ProfileIds.SoundsEnabled).Value;
            if (!enabled)
            {
                return UniTask.CompletedTask;
            }
            var result = sounds.PlaySound(id, prepare);
            return result;
        }

        public static UniTask PlayMusicByEnable(this ISounds sounds, string id, IProfile profile, Action<ISound> prepare = null)
        {
            var enabled = profile.Property<bool>(ProfileIds.MusicEnabled).Value;
            if (!enabled)
            {
                return UniTask.CompletedTask;
            }
            var result = sounds.PlaySound(id, prepare);
            return result;
        }
    }
}
