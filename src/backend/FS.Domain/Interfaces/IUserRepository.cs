namespace FS.Domain.Core.Interfaces
{
    using FS.Domain.Model;
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository : IGet<User>, ICreate<User>, IDelete<User>, IUpdate<User>
    {
        Task<Guid> GetUserByUsernameAndPassword(string name, string password);
    }
}