using UnityEngine;

namespace MassiveCore.Framework
{
    public class DebugLogger : ILogger
    {
        public void Print(string text)
        {
            Debug.Log(text);
        }

        public void PrintError(string text)
        {
            Debug.LogError(text);
        }

        public void PrintWarning(string text)
        {
            Debug.LogWarning(text);
        }
    }
}
