using FS.Domain.Core.Interfaces;
using FS.Domain.Model;
using System;

namespace FS.Domain.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>, IDisposable
    {
      
    }
}