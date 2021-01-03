namespace FS.Domain.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FS.Domain.Model;

    public interface IMovimentRepository : IGet<Moviment>, ICreate<Moviment>, IDelete<Moviment>, IUpdate<Moviment>
    {
        Task<IEnumerable<Moviment>> GetMovimentsByAccount(Guid userId, Guid accountId, int page, int size);
    }
}