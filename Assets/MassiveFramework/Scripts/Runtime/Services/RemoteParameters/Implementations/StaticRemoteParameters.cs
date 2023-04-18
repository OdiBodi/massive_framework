using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "static_remote_parameters_config", menuName = "Massive Framework/Configs/Static Remote Parameters")]
    public class StaticRemoteParameters : Config, IRemoteParameters
    {
        [SerializeField, TableList]
        private RemoteParameter[] _parameters;

        public string this[string name]
        {
            get
            {
                var parameter = Parameter(name);
                return parameter?.LocalValue;
            }
        }

        public async UniTask<bool> Fetch()
        {
            return true;
        }

        public IRemoteParameter Parameter(string name)
        {
            var parameter = _parameters.FirstOrDefault(p => p.Name == name);
            return parameter;
        }
    }
}
