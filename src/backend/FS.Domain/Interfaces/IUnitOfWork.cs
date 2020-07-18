using System.Threading.Tasks;

namespace FS.Domain.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}