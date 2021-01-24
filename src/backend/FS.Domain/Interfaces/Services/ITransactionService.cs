namespace FS.Domain.Core.Interfaces.Services
{
    using System.Threading.Tasks;
    using Model;
    using System;

    public interface ITransactionService
    {
        Task<Balance> CreateOrUpdateBalance(Moviment moviment);

        Task<Balance> UpdateMovimentAndUpdateBalance(Guid userId, Guid accountId, Guid movimentId,
            decimal value, string description = null);
    }
}