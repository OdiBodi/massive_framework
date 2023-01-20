using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MassiveCore.Framework.Runtime
{
    [CreateAssetMenu(fileName = "player_loop_config", menuName = "Massive Framework/Configs/Player Loop Config")]
    public class PlayerLoopConfig : Config
    {
        [SerializeField, Toggle("enabled")]
        private PlayerLoopPhase _initializationPhase;
        
        [SerializeField, Toggle("enabled")]
        private PlayerLoopPhase _timeUpdatePhase;
        
        [SerializeField, Toggle("enabled")]
        private PlayerLoopPhase _fixedUpdatePhase;
        
        [SerializeField, Toggle("enabled")]
        private PlayerLoopPhase _earlyUpdatePhase;
        
        [SerializeField, Toggle("enabled")]
        private PlayerLoopPhase _preUpdatePhase;
        
        [SerializeField, Toggle("enabled")]
        private PlayerLoopPhase _updatePhase;
        
        [SerializeField, Toggle("enabled")]
        private PlayerLoopPhase _preLateUpdatePhase;
        
        [SerializeField, Toggle("enabled")]
        private PlayerLoopPhase _postLateUpdatePhase;

        public PlayerLoopPhase InitializationPhase => _initializationPhase;
        public PlayerLoopPhase TimeUpdatePhase => _timeUpdatePhase;
        public PlayerLoopPhase FixedUpdatePhase => _fixedUpdatePhase;
        public PlayerLoopPhase EarlyUpdatePhase => _earlyUpdatePhase;
        public PlayerLoopPhase PreUpdatePhase => _preUpdatePhase;
        public PlayerLoopPhase UpdatePhase => _updatePhase;
        public PlayerLoopPhase PreLateUpdatePhase => _preLateUpdatePhase;
        public PlayerLoopPhase PostLateUpdatePhase => _postLateUpdatePhase;

        public IEnumerable<PlayerLoopPhase> Phases
        {
            get
            {
                yield return _initializationPhase;
                yield return _timeUpdatePhase;
                yield return _fixedUpdatePhase;
                yield return _earlyUpdatePhase;
                yield return _preUpdatePhase;
                yield return _updatePhase;
                yield return _preLateUpdatePhase;
                yield return _postLateUpdatePhase;
            }
        }

        [OnInspectorInit]
        private void Initialization()
        {
            InitializePhase<Initialization>(ref _initializationPhase);
            InitializePhase<TimeUpdate>(ref _timeUpdatePhase);
            InitializePhase<FixedUpdate>(ref _fixedUpdatePhase);
            InitializePhase<EarlyUpdate>(ref _earlyUpdatePhase);
            InitializePhase<PreUpdate>(ref _preUpdatePhase);
            InitializePhase<Update>(ref _updatePhase);
            InitializePhase<PreLateUpdate>(ref _preLateUpdatePhase);
            InitializePhase<PostLateUpdate>(ref _postLateUpdatePhase);
        }

        private void InitializePhase<T>(ref PlayerLoopPhase phase)
        {
            if (phase.Valid)
            {
                return;
            }
            phase.enabled = true;
            phase.typeAssemblyQualifiedName = typeof(T).AssemblyQualifiedName;
            phase.systems = Types<T>().Select(type => new PlayerLoopSystem
            {
                enabled = true,
                typeName = type.Name,
                typeAssemblyQualifiedName = type.AssemblyQualifiedName
            }).ToArray();
        }

        private IEnumerable<Type> Types<T>()
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic;
            var types = typeof(T).GetNestedTypes(flags);
            return types;
        }
    }
}
