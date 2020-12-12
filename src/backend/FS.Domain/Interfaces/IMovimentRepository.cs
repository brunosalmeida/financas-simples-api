using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FS.Domain.Model;

namespace FS.Domain.Core.Interfaces
{
    public interface IMovimentRepository : IGet<Moviment>, ICreate<Moviment>, IDelete<Moviment>, IUpdate<Moviment>
    {
        Task<IEnumerable<Moviment>> GetExpensesByAccount(Guid accountId);
    }
}