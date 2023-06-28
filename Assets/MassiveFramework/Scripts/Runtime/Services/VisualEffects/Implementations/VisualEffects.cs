using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class VisualEffects : IVisualEffects
    {
        [Inject]
        private readonly ILogger _logger;

        [Inject]
        private readonly IConfigs _configs;

        [Inject]
        private readonly IPool _pool;

        private readonly List<IVisualEffect> _visualEffects = new(8);

        private readonly WaitingList<string> _waitingList = new(8);

        public IEnumerable<IVisualEffect> VisualEffectsBy(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return _visualEffects;
            }
            return _visualEffects.Where(sound => sound.Id == id);
        }

        public UniTask PlayVisualEffect(string id, Action<IVisualEffect> prepare)
        {
            if (!VisualEffectAvailabilityByCooldownTime(id))
            {
                _logger.Print($"Visual effect \"{id}\" is not available by cooldown time!");
                return UniTask.CompletedTask;
            }

            var visualEffect = _pool.Request<VisualEffect>(id);

            if (!_visualEffects.Contains(visualEffect))
            {
                _visualEffects.Add(visualEffect);
            }

            prepare?.Invoke(visualEffect);

            var task = visualEffect.Play();

            task.ContinueWith(() =>
            {
                _visualEffects.Remove(visualEffect);
                _pool.Return(visualEffect);
            });

            return task;
        }

        public void StopVisualEffects()
        {
            VisualEffectsBy().ForEach(sound => sound.Stop());
        }

        private bool VisualEffectAvailabilityByCooldownTime(string id)
        {
            var configs = _configs.Config<VisualEffectsConfig>().Configs;
            var config = configs.First(x => x.Id == id);
            var result = config.CooldownTime <= 0f || _waitingList.AddItem(id, config.CooldownTime);
            return result;
        }
    }
}
