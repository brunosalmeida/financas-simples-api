namespace FS.Domain.Core.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using FS.Domain.Model;

    public interface IAccountRepository : IGet<Account>, ICreate<Account>, IDelete<Account>
    {
        Task<Account> GetAccountByUserId(Guid userId);
    }
}