namespace FS.Domain.Core.Interfaces.Services
{
    using System.Threading.Tasks;
    using DataObject.User;
    using Model;

    public interface IUserAccountService
    {
        Task<UserAccount> CreateUserAndAccount(User user);
    }
}