using System;
using UnityEngine;

namespace MassiveCore.Framework
{
    public static class DateTimeExtensions
    {
        public static string ToBinaryString(this DateTime dateTime)
        {
            return dateTime.ToBinary().ToString(); 
        }

        public static DateTime FromPlayerPrefs(this DateTime defaultValue, string key)
        {
            var binaryString = PlayerPrefs.GetString(key, defaultValue.ToBinaryString());
            var binary = long.Parse(binaryString);
            return DateTime.FromBinary(binary);
        }

        public static void SaveToPlayerPrefs(this DateTime dateTime, string key)
        {
            PlayerPrefs.SetString(key, dateTime.ToBinaryString());
        }
    }
}
