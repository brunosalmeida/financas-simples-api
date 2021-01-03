namespace FS.Domain.Core.Interfaces.Services
{
    using System.Threading.Tasks;
    using Model;

    public interface ITransactionService
    {
        Task<Balance> CreateOrUpdateBalance(Moviment moviment);
      
    }
}