using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public class GameSessionInitializer : ServiceInitializer
    {
        public override async UniTask<bool> Initialize()
        {
            return true;
        }
    }
}
