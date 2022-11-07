using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IVisualEffects
    {
        IEnumerable<IVisualEffect> VisualEffectsBy(string id = "");
        UniTask PlayVisualEffect(string id, Action<IVisualEffect> prepare = null);
        void StopVisualEffects();
    }
}
