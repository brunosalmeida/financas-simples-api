namespace FS.Domain.Core.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IBalanceRepository :IUpdate<FS.Domain.Model.Balance>,
        ICreate<FS.Domain.Model.Balance>
    {
        public Task<FS.Domain.Model.Balance> Get(Guid userId, Guid accountId);
    }
}