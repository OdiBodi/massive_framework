using System.Collections.Generic;

namespace MassiveCore.Framework.Runtime
{
    public static class ScreensExtensions
    {
        public static void UpdateOrders(this IEnumerable<Screen> screens, int originOrder)
        {
            var order = originOrder;
            screens.ForEach(screen => screen.Order = order++);
        }
    }
}
