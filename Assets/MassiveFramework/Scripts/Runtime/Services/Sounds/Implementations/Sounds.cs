using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class Sounds : ISounds
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IConfigs _configs;

        [Inject]
        private readonly IPool _pool;

        private readonly List<ISound> _sounds = new(8);

        private readonly WaitingList<string> _waitingList = new(8);

        public IEnumerable<ISound> SoundsBy(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return _sounds;
            }
            return _sounds.Where(sound => sound.Id == id);
        }

        public UniTask PlaySound(string id, Action<ISound> prepare)
        {
            if (!SoundAvailabilityByCooldownTime(id))
            {
                _logger.Print($"Sound \"{id}\" is not available by cooldown time!");
                return UniTask.CompletedTask;
            }

            var sound = _pool.Request<Sound>(id);

            if (!_sounds.Contains(sound))
            {
                _sounds.Add(sound);
            }

            prepare?.Invoke(sound);

            var task = sound.Play();

            task.ContinueWith(() =>
            {
                _sounds.Remove(sound);
                _pool.Return(sound);
            });

            return task;
        }

        public void StopSounds()
        {
            SoundsBy().ForEach(sound => sound.Stop());
        }

        private bool SoundAvailabilityByCooldownTime(string id)
        {
            var configs = _configs.Config<SoundsConfig>().Configs;
            var config = configs.First(x => x.Id == id);
            var result = config.CooldownTime <= 0f || _waitingList.AddItem(id, config.CooldownTime);
            return result;
        }
    }
}
