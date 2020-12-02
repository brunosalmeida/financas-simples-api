using System;
using System.Linq;
using System.Threading.Tasks;
using FS.Data.Mappings;
using FS.Domain.Core.Interfaces;
using FS.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FS.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _context;

        public AccountRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Account> Get(Guid id)
        {
            var entity = await this._context.Accounts.AsNoTracking().
                Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();

            return AccountEntityToAccountDomainMapper.MapFrom(entity);
        }

        public async Task Insert(Account entity)
        {
            await this._context.Accounts.AddAsync(AccountDomainToAccountEntityMapper.MapFrom(entity));

            await this._context.SaveChangesAsync();
        }

        public Task Update(Guid id, Account model)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task Delete(Guid id)
        {
            var entity = await this._context.Accounts.FirstOrDefaultAsync(u => u.Id.Equals(id));
            this._context.Accounts.Remove(entity);

            await this._context.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public async Task<Account> GetAccountByUserId(Guid userId)
        {
            var entity = await this._context.Accounts.AsNoTracking()
                .Where(u => u.UserId.Equals(userId)).FirstOrDefaultAsync();
            
            return AccountEntityToAccountDomainMapper.MapFrom(entity);
        }
    }
}