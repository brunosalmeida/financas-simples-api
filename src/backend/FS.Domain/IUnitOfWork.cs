using System.Threading.Tasks;

namespace FS.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}