namespace FS.Domain.Core.Interfaces
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IInvestmentRepository : IGet<Investment>, ICreate<Investment>, IUpdate<Investment>
    {
        Task<IEnumerable<Investment>> GetInvestmentsByAccount(Guid userId, Guid accountId, int page, int size);
    }
}