namespace MassiveCore.Framework.Runtime
{
    public class SafeArea : BaseMonoBehaviour
    {
        private void Awake()
        {
            var safeArea = UnityEngine.Screen.safeArea;
            var screenWidth = UnityEngine.Screen.width;
            var screenHeight = UnityEngine.Screen.height;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= screenWidth;
            anchorMin.y /= screenHeight;
            anchorMax.x /= screenWidth;
            anchorMax.y /= screenHeight;

            CacheRectTransform.anchorMin = anchorMin;
            CacheRectTransform.anchorMax = anchorMax;
        }
    }
}
