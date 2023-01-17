using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public enum Framerate
    {
        [InspectorName("-1"), Number(-1)]
        _1,
        [InspectorName("30"), Number(30)]
        _30,
        [InspectorName("60"), Number(60)]
        _60
    }
}
