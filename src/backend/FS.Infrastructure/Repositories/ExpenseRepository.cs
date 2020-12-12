namespace FS.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FS.Domain.Core.Interfaces;
    using FS.Domain.Model;
    using System.Data;

    public class ExpenseRepository : IMovimentRepository
    {
        private IDbConnection _connection;
        
        public ExpenseRepository()
        {
            
        }

        public async Task<Moviment> Get(Guid id)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task<IEnumerable<Moviment>> GetExpensesByAccount(Guid accountId)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task Insert(Moviment entity)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task Update(Guid id, Moviment model)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException("Method available");
        }
        
        
    }
}