using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FS.Domain.Model;

namespace FS.Domain.Core.Interfaces
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetExpensesByAccount(Guid accountId);
    }
}