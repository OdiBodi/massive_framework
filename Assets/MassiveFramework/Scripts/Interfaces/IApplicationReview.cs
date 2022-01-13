using System.Threading.Tasks;

namespace MassiveCore.Framework
{
    public interface IApplicationReview
    {
        Task<bool> Request();
    }
}
