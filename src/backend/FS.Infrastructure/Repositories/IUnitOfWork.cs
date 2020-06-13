using System.Threading.Tasks;

namespace FS.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}