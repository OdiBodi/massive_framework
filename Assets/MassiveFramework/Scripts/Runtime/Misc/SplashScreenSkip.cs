using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace MassiveCore.Framework.Runtime
{
    public class SplashScreenSkip
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void BeforeSplashScreen()
        {
#if UNITY_WEBGL
            Application.focusChanged += OnApplicationFocusChanged;
#else
            Task.Run(StopSplashScreen);
#endif
        }

#if UNITY_WEBGL
        private static void OnApplicationFocusChanged(bool result)
        {
            Application.focusChanged -= OnApplicationFocusChanged;
            StopSplashScreen();
        }
#endif

        private static void StopSplashScreen()
        {
            SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
        }
    }
}
