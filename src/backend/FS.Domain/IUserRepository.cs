using FS.Domain.Model;
using System;

namespace FS.Domain.Core
{
    public interface IUserRepository : IRepository<User>, IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}