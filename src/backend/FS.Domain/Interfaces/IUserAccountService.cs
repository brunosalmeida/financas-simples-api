namespace FS.Domain.Core.Interfaces
{
    using System.Threading.Tasks;
    using DataObject.User;
    using Model;

    public interface IUserAccountService
    {
        Task<UserAccount> Create(User user);
    }
}