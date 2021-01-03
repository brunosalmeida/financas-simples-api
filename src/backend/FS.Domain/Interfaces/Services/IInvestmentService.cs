namespace FS.Domain.Core.Interfaces.Services
{
    using Model;
    using System.Threading.Tasks;

    public interface IInvestmentService
    {
        Task<Balance> CreateOrUpdateBalance(Investment moviment);
    }
}