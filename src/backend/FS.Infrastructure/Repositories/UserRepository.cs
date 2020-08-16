using FS.Domain.Core.Interfaces;
using FS.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FS.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task Delete(Guid id)
        {
            var entity = await this._context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            this._context.Users.Remove(entity);

            await this._context.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public async Task<Domain.Model.User> Get(Guid id)
        {
            var entity = await this._context.Users.Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();

            return UserEntityToUserDomainMapper.MapFrom(entity);
        }

        public async Task<IEnumerable<Domain.Model.User>> GetAll()
        {
            var entities = await this._context.Users.ToListAsync();

            return entities.Select(UserEntityToUserDomainMapper.MapFrom);
        }

        public async Task Insert(Domain.Model.User entity)
        {
            await this._context.Users.AddAsync(UserDomainToUserEntityMapper.MapFrom(entity));

            await this._context.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public async Task Update(Guid id, Domain.Model.User model)
        {
            var entity = UserDomainToUserEntityMapper.MapFrom(model);
            var user = this._context.Users.FirstOrDefault(u => u.Id.Equals(id));

            if (user != null)
            {
                user.Name = entity.Name;
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.CreatedOn = entity.CreatedOn;
                user.UpdatedOn = entity.UpdatedOn;

                this._context.Users.Update(user);
            }

            await this._context.SaveChangesAsync();

            await Task.CompletedTask;
        }
    }
   
}