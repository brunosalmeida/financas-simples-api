using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FS.Domain.Model;

namespace FS.Domain.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);

        Task Insert(T entity);

        Task Update(Guid id, T entity);

        Task Delete(Guid id);
    }
}