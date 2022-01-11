using UnityEngine;

namespace MassiveCore.Framework
{
    public class DebugLogger : ILogger
    {
        public void Print(string text)
        {
            Debug.Log(text);
        }
    }
}
