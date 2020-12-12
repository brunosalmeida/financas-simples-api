using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FS.Domain.Model;

namespace FS.Domain.Core.Interfaces
{
    using System.Data;

    public interface IDelete<T> where T : class
    {
        Task Delete(Guid id);
    }

    public interface IGet<T> where T : class
    {
        Task<T> Get(Guid id);
    }
    
    public interface ICreate<T> where T : class
    {
        Task Insert(T entity);
    }
    
    public interface IUpdate<T> where T : class
    {
        Task Update(Guid id, T entity);
    }
}