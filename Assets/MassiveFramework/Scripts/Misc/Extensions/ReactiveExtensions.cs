using System;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class ReactiveExtensions
    {
        public static void GetPlayerPrefsBool(this ReactiveProperty<bool> property, string key, bool defaultValue)
        {
            property.Value = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }
        
        public static void GetPlayerPrefsInt(this ReactiveProperty<int> property, string key, int defaultValue)
        {
            property.Value = PlayerPrefs.GetInt(key, defaultValue);
        }
        
        public static void GetPlayerPrefsFloat(this ReactiveProperty<float> property, string key, float defaultValue)
        {
            property.Value = PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static void GetPlayerPrefsString(this ReactiveProperty<string> property, string key, string defaultValue)
        {
            property.Value = PlayerPrefs.GetString(key, defaultValue);
        }

        public static void GetPlayerPrefsDateTime(this ReactiveProperty<DateTime> property, string key, DateTime defaultValue)
        {
            property.Value = defaultValue.FromPlayerPrefs(key);
        }

        public static void GetPlayerPrefsCollection<T>(this ReactiveCollection<T> property, string key)
        {
            var json = PlayerPrefs.GetString(key, "[]");
            var collection = JsonConvert.DeserializeObject<ReactiveCollection<T>>(json);
            property.Clear();
            collection.ForEach(property.Add);
        }

        public static void SetPlayerPrefsBool(this ReactiveProperty<bool> property, string key)
        {
            PlayerPrefs.SetInt(key, property.Value ? 1 : 0);
        }
        
        public static void SetPlayerPrefsInt(this ReactiveProperty<int> property, string key)
        {
            PlayerPrefs.SetInt(key, property.Value);
        }
        
        public static void SetPlayerPrefsFloat(this ReactiveProperty<float> property, string key)
        {
            PlayerPrefs.SetFloat(key, property.Value);
        }
        
        public static void SetPlayerPrefsString(this ReactiveProperty<string> property, string key)
        {
            PlayerPrefs.SetString(key, property.Value);
        }
        
        public static void SetPlayerPrefsDateTime(this ReactiveProperty<DateTime> property, string key)
        {
            var binaryString = property.Value.ToBinaryString();
            PlayerPrefs.SetString(key, binaryString);
        }
        
        public static void SetPlayerPrefsCollection<T>(this ReactiveCollection<T> property, string key)
        {
            var json = JsonConvert.SerializeObject(property);
            PlayerPrefs.SetString(key, json);
        }

        public static void Max(this ReactiveProperty<int> property, int value)
        {
            property.Value = Math.Max(value, property.Value);
        }
    }
}
