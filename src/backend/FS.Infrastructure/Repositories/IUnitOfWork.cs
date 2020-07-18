using System.Threading.Tasks;

namespace FS.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}