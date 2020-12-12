using FS.Domain.Core.Interfaces;
using FS.Domain.Model;

namespace FS.Domain.Core.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IAccountRepository : IGet<Account>, ICreate<Account>, IDelete<Account>
    {
        Task<Account> GetAccountByUserId(Guid userId);
    }
}