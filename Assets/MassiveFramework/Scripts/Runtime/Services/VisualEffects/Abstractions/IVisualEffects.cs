using System;
using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IVisualEffects
    {
        UniTask PlayVisualEffect(string id, Action<IVisualEffect> prepare);
    }
}
