namespace FS.Domain.Core.Interfaces.Services
{
    using System.Threading.Tasks;
    using Model;
    using System;

    public interface ITransactionService
    {
        Task<Balance> CreateOrUpdateBalance(Movement movement);

        Task<Balance> UpdateMovementAndUpdateBalance(Guid userId, Guid accountId, Guid movementId,
            decimal value, string description = null);
    }
}