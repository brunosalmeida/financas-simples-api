using FS.Domain.Core.Interfaces;
using FS.Domain.Model;

namespace FS.Domain.Core.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountByUserId(Guid userId);
    }
}