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
            await _remoteParameters.Fetch();
            CompleteInitialize(true);
            return await base.Initialize();
        }
    }
}
