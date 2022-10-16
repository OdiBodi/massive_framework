using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework
{
    [CreateAssetMenu(fileName = "remote_parameters", menuName = "Massive Framework/Configs/Remote Parameters")]
    public class FirebaseRemoteParameters : Config, IRemoteParameters
    {
        [SerializeField]
        private bool _updatable = true;

        [SerializeField, TableList]
        private RemoteParameter[] _parameters;

        [NonSerialized]
        private RemoteParameter[] _resultParameters;

#if UNITY_EDITOR
        [NonSerialized]
        private bool _downloading;
#endif

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
#if DEBUG
            var logger = new DebugLogger();
#else
            var logger = new ReleaseLogger();
#endif
            var firebase = new Firebase(logger);
            var fetchResult = await firebase.FetchRemoteConfig();
            if (!fetchResult)
            {
                return false;
            }
            var merger = new RemoteParameterMerger();
            _resultParameters = _updatable ? ResultParameters(firebase, merger).ToArray() : _parameters;
            return true;
        }

        public IRemoteParameter Parameter(string name)
        {
            var parameter = _resultParameters.FirstOrDefault(p => p.Name == name);
            return parameter;
        }

#if UNITY_EDITOR
        [DisableIf("_downloading", true)]
        [BoxGroup("Download", CenterLabel = true), ButtonGroup("Download/Buttons"), Button("Replace", ButtonSizes.Large)]
        private async void DownloadReplace()
        {
            _downloading = true;

            var firebase = new Firebase(new DebugLogger());
            if (!await firebase.Initialize())
            {
                return;
            }
            if (!await firebase.FetchRemoteConfig())
            {
                return;
            }
            _parameters = firebase.Values((key, value) =>
                new RemoteParameter(key, value, value, true)).ToArray();

            _downloading = false;
        }

        [DisableIf("_downloading", true)]
        [ButtonGroup("Download/Buttons"), Button("Merge", ButtonSizes.Large)]
        private async void DownloadMerge()
        {
            _downloading = true;

            var firebase = new Firebase(new DebugLogger());
            if (!await firebase.Initialize())
            {
                return;
            }
            if (!await firebase.FetchRemoteConfig())
            {
                return;
            }
            var merger = new RemoteParameterMerger();
            _parameters = ResultParameters(firebase, merger).ToArray();

            _downloading = false;
        }
#endif // UNITY_EDITOR

        private List<RemoteParameter> ResultParameters(Firebase firebase, IRemoteParameterMerger merger)
        {
            var resultParameters = _parameters.ToList();
            var fetchedParameters = firebase.Values((key, value) =>
                new RemoteParameter(key, value, value, true));

            foreach (var fetchedParameter in fetchedParameters)
            {
                var resultParameter = resultParameters.FirstOrDefault(p => p.Name == fetchedParameter.Name);
                if (resultParameter == null)
                {
                    resultParameters.Add(fetchedParameter);
                }
                else
                {
                    resultParameter.Merge(fetchedParameter, merger);
                }
            }

            return resultParameters;
        }
    }
}
