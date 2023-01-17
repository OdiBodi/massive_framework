using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;

namespace MassiveCore.Framework.Runtime
{
    public class Firebase
    {
        private readonly ILogger _logger;

        public Firebase(ILogger logger)
        {
            _logger = logger;
        }

        public async UniTask<bool> Initialize()
        {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyStatus != DependencyStatus.Available)
            {
                _logger.PrintError($"Firebase: Could not resolve all dependencies! Status is {dependencyStatus}");
                return false;
            }
            _logger.Print("Firebase: Initialized!");
            return true;
        }

        public async UniTask<bool> FetchRemoteConfig()
        {
            var instance = FirebaseRemoteConfig.DefaultInstance;

            try
            {
                await instance.FetchAsync(TimeSpan.Zero);
            }
            catch (Exception)
            {
            }

            var lastFetchStatus = instance.Info.LastFetchStatus;
            _logger.Print($"Firebase: Remote config fetch result is {lastFetchStatus}");

            if (instance.Info.LastFetchStatus == LastFetchStatus.Success)
            {
                await instance.ActivateAsync();
                foreach (var (key, value) in instance.AllValues)
                {
                    var stringValue = value.StringValue;
                    _logger.Print($"Firebase: \"{key}\"=\"{stringValue}\"");
                }
                _logger.Print("Firebase: Remote config loaded!");
                return true;
            }

            if (instance.Info.LastFetchStatus == LastFetchStatus.Failure)
            {
                var lastFetchFailureReason = instance.Info.LastFetchFailureReason;
                _logger.Print($"Firebase: Remote config fetch failure reason is {lastFetchFailureReason}");
            }

            return false;
        }

        public IEnumerable<T> Values<T>(Func<string, string, T> cast)
        {
            var allValues = FirebaseRemoteConfig.DefaultInstance.AllValues;
            var values = allValues.Select(value => cast(value.Key, value.Value.StringValue));
            return values;
        }
    }
}
