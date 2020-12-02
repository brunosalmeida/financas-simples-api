namespace FS.Domain.Core.Services
{
    using System.Threading.Tasks;
    using DataObject.User;
    using Model;

    public interface IUserAccountService
    {
        Task<UserAccount> Create(User user);
    }
}