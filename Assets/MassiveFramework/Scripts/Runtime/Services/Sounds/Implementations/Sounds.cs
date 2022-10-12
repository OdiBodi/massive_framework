using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class Sounds : ISounds
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IConfigs _configs;

        [Inject]
        private readonly IPool _pool;

        private readonly WaitingList<string> _waitingList = new(8);

        public IEnumerable<ISound> SoundsBy(string id)
        {
            var sounds = _pool.Objects<Sound>(id);
            return sounds;
        }

        public UniTask PlaySound(string id, Action<ISound> prepare)
        {
            if (!SoundAvailabilityByCooldownTime(id))
            {
                _logger.Print($"Sound \"{id}\" is not available by cooldown time!");
                return UniTask.CompletedTask;
            }
            var sound = SoundBy(id);
            prepare?.Invoke(sound);
            var result = sound.Play();
            return result;
        }

        private ISound SoundBy(string id)
        {
            var sound = _pool.Request<Sound>(id);
            return sound;
        }

        private bool SoundAvailabilityByCooldownTime(string id)
        {
            var configs = _configs.Config<SoundsConfig>().Configs;
            var config = configs.First(x => x.Id == id);
            var result = config.CooldownTime <= 0f || _waitingList.Add(id, config.CooldownTime);
            return result;
        }
    }
}
