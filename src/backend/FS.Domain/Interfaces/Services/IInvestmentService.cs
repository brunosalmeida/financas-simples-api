namespace FS.Domain.Core.Interfaces.Services
{
    using Model;
    using System;
    using System.Threading.Tasks;
    using Utils.Enums;

    public interface IInvestmentService
    {
        Task<Balance> CreateAndUpdateBalance(Investment moviment);

        Task<Balance> UpdateInvestmentAndUpdateBalance(Guid userId, Guid accountId, Guid investmentId,
            decimal value, string description = null, EInvestmentType? type = null);
    }
}