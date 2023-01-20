using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class PlayerLoopInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IConfigs _configs;

        private PlayerLoopConfig Config => _configs.Config<PlayerLoopConfig>(); 

        public override UniTask<bool> Initialize()
        {
            new CustomPlayerLoop(Config).Apply();
            CompleteInitialize(true);
            return base.Initialize();
        }
    }
}
