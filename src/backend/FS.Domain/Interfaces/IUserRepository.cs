using FS.Domain.Core.Interfaces;
using FS.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FS.Domain.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>, IDisposable
    {
        Task<Guid> GetUserByUsernameAndPassword(string name, string password);
    }
}