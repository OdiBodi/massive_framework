using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassiveCore.Framework
{
    [Serializable]
    public class RemoteParameter : IRemoteParameter
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private string _localValue;

        [SerializeField, ReadOnly]
        private string _remoteValue;

        [SerializeField]
        private bool _updatable = true;

        public RemoteParameter(string name, string localValue, string remoteValue, bool updatable)
        {
            _name = name;
            _localValue = localValue;
            _remoteValue = remoteValue;
            _updatable = updatable;
        }

        public string Name => _name;
        public string LocalValue
        {
            get => _localValue;
            set => _localValue = value;
        }
        public string RemoteValue => _remoteValue;
        public bool Updatable => _updatable;

        public void Merge(IRemoteParameter other, IRemoteParameterMerger merger)
        {
            if (_name != other.Name || !_updatable)
            {
                return;
            }
            var (localValue, remoteValue) = merger.Merge(this, other);
            _localValue = localValue;
            _remoteValue = remoteValue;
        }
    }
}
