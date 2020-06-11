using FS.Domain;
using FS.Domain.Core;
using FS.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext context;

        public UserRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IUnitOfWork UnitOfWork => this.context;

        public async Task Delete(Guid id)
        {
            var entity = await this.context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            this.context.Users.Remove(entity);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public async Task<Domain.Model.User> Get(Guid id)
        {
            var entity = await this.context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));

            return UserEntityToUserDomainMapper.MapFrom(entity);
        }

        public async Task<IEnumerable<Domain.Model.User>> GetAll()
        {
            var entities = await this.context.Users.ToListAsync();

            return entities.Select(e => UserEntityToUserDomainMapper.MapFrom(e));
        }

        public Task<IEnumerable<Domain.Model.User>> GetAll(Func<Expression, IEnumerable<Domain.Model.User>> action)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Domain.Model.User entity)
        {
            this.context.Users.Add(UserDomainToUserEntityMapper.MapFrom(entity));
            return Task.CompletedTask;
        }

        public Task Update(Guid id, Domain.Model.User model)
        {
            var entity =UserDomainToUserEntityMapper.MapFrom(model);
            var user = this.context.Users.FirstOrDefault(u => u.Id.Equals(id));

            if(user != null)
            {
                user.Name = entity.Name;
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.CreatedOn = entity.CreatedOn;
                user.UpdatedOn = entity.UpdatedOn;

                this.context.Users.Update(user);
            }

            return Task.CompletedTask;
        }
    }
}