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

        private readonly WaitingList<string> _waitingList = new(8);

        public IEnumerable<IVisualEffect> VisualEffectsBy(string id)
        {
            var visualEffects = _pool.Objects<VisualEffect>(id);
            return visualEffects;
        }

        public UniTask PlayVisualEffect(string id, Action<IVisualEffect> prepare)
        {
            if (!EffectAvailabilityByCooldownTime(id))
            {
                _logger.Print($"Visual effect \"{id}\" is not available by cooldown time!");
                return UniTask.CompletedTask;
            }
            var effect = EffectBy(id);
            prepare?.Invoke(effect);
            var result = effect.Play();
            return result;
        }

        public void StopVisualEffects()
        {
            _pool.ReturnAll<VisualEffect>();
        }

        private IVisualEffect EffectBy(string id)
        {
            var effect = _pool.Request<VisualEffect>(id);
            return effect;
        }

        private bool EffectAvailabilityByCooldownTime(string id)
        {
            var configs = _configs.Config<VisualEffectsConfig>().Configs;
            var config = configs.First(x => x.Id == id);
            var result = config.CooldownTime <= 0f || _waitingList.AddItem(id, config.CooldownTime);
            return result;
        }
    }
}
