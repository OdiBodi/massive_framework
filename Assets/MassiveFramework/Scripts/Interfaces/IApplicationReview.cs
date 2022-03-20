using Cysharp.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IApplicationReview
    {
        UniTask<bool> Request();
    }
}
