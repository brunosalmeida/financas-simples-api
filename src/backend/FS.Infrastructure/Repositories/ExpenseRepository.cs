using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FS.Data.Mappings;
using FS.Domain.Core.Interfaces;
using FS.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FS.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly DatabaseContext _context;
        
        public ExpenseRepository(DatabaseContext context)
        {
            this._context = context;   
        }

        public async Task<Expense> Get(Guid id)
        {
            var entity = await this._context.Expenses.AsNoTracking().Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();

            return ExpenseEntityToExpenseDomainMapper.MapFrom(entity);
        }

        public async Task<IEnumerable<Expense>> GetExpensesByAccount(Guid accountId)
        {
            var entity = await this._context.Expenses.AsNoTracking().Where(u => u.AccountId.Equals(accountId)).ToListAsync();
            
            return ExpenseEntityToExpenseDomainMapper.MapFrom(entity);
        }

        public async Task Insert(Expense entity)
        {
            await this._context.Expenses.AddAsync(ExpenseDomainToExpenseEntityMapper.MapFrom(entity));

            await this._context.SaveChangesAsync();
            
            await Task.CompletedTask;
        }

        public async Task Update(Guid id, Expense model)
        {
            var entity = ExpenseDomainToExpenseEntityMapper.MapFrom(model);
            var expense = await this._context.Expenses.AsNoTracking().Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();

            if (entity != null)
            {
                expense.Description = entity.Description;
                expense.Value = entity.Value;
                expense.UpdatedOn = expense.UpdatedOn;
            }
            
            await Task.CompletedTask;
        }

        public async Task Delete(Guid id)
        {
            var entity = await this._context.Expenses.FirstOrDefaultAsync(u => u.Id.Equals(id));
            this._context.Expenses.Remove(entity);

            await this._context.SaveChangesAsync();

            await Task.CompletedTask;
        }
        
        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}