using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework.Runtime
{
    public interface IApplicationReview
    {
        UniTask<bool> Request();
    }
}
