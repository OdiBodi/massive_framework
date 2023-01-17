using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework.Runtime
{
    public class FirebaseInitializer : ServiceInitializer
    {
        [Inject]
        private readonly ILogger _logger;

        public override async UniTask<bool> Initialize()
        {
            var firebase = new Firebase(_logger);
            var result = await firebase.Initialize();
            CompleteInitialize(result);
            return result;
        }
    }
}
