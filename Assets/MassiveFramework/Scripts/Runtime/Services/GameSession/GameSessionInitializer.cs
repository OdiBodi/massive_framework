using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public class GameSessionInitializer : ServiceInitializer
    {
        public override UniTask<bool> Initialize()
        {
            CompleteInitialize(true);
            return base.Initialize();
        }
    }
}
