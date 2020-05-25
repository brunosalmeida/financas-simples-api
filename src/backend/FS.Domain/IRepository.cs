using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FS.Domain
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(Func<Expression, IEnumerable<T>> action);

        Task Insert(T entity);

        Task Update(Guid id, T entity);

        Task Delete(Guid id);
    }
}