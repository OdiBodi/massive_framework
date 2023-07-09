using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime
{
    public interface ISounds
    {
        IEnumerable<ISound> SoundsBy(string id = "");
        UniTask PlaySound(string id, Action<ISound> prepare = null);
        void StopSounds();
    }
}
