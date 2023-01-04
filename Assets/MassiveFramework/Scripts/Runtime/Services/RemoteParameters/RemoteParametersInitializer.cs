using Cysharp.Threading.Tasks;
using Zenject;

namespace MassiveCore.Framework
{
    public class RemoteParametersInitializer : ServiceInitializer
    {
        [Inject]
        private readonly IRemoteParameters _remoteParameters;

        public override async UniTask<bool> Initialize()
        {
            var result = await _remoteParameters.Fetch();
            CompleteInitialize(result);
            return await base.Initialize();
        }
    }
}
