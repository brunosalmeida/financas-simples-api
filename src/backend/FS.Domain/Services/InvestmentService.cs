// unset

namespace FS.Domain.Core.Services
{
    using Interfaces;
    using Interfaces.Services;
    using Model;
    using System;
    using System.Threading.Tasks;
    using Utils.Enums;

    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestmentBalanceRepository _investmentBalanceRepository;
        private readonly IInvestmentRepository _investmentRepository;
        private readonly ITransactionService _transactionService;

        public InvestmentService(IInvestmentBalanceRepository investmentBalanceRepository,
            IInvestmentRepository investmentRepository,
            ITransactionService transactionService)
        {
            _investmentBalanceRepository = investmentBalanceRepository;
            _investmentRepository = investmentRepository;
            _transactionService = transactionService;
        }


        public async Task<Balance> CreateAndUpdateBalance(Investment investment)
        {
            try
            {
                var moviment = new Moviment(investment.Value, investment.Description, EMovimentCategory.Investment,
                    EMovimentType.Investment, investment.AccountId, investment.UserId);

                var userBalance = await _transactionService.CreateOrUpdateBalance(moviment);

                investment.SetMoviment(moviment.Id);
                await CreateInvestment(investment);

                var userInvestmentBalance =
                    await _investmentBalanceRepository.Get(investment.UserId, investment.AccountId);

                if (userInvestmentBalance is null)
                {
                    return await FirstInvestmentBalance(investment.UserId, investment.AccountId, investment.Value);
                }

                userInvestmentBalance.UpdateBalance(investment.Value);
                await UpdateInvestmentBalance(userInvestmentBalance);

                return userBalance;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Balance> UpdateInvestmentAndUpdateBalance(Guid userId, Guid accountId, Guid investmentId,
            decimal value, string description = null, EInvestmentType? type = null)
        {
            var investment = await _investmentRepository.Get(investmentId);
            var userBalance = await _investmentBalanceRepository.Get(userId, accountId);

            var valueToRemove = investment.Value * -1;

            investment.SetValue(value);

            if (description != null) investment.SetDescription(description);

            if (type != null) investment.SetInvestmentType(type.Value);

            investment.SetUpdateDate();

            await _investmentRepository.Update(investmentId, investment);

            userBalance = await _transactionService.UpdateMovimentAndUpdateBalance(userId, accountId,
                investment.MovimentId,
                investment.Value,
                investment.Description);

            return userBalance;
        }

        private async Task<Balance> FirstInvestmentBalance(Guid userId, Guid accountId, decimal value)
        {
            try
            {
                var balance = new Balance(userId, accountId, value);
                await _investmentBalanceRepository.Insert(balance);

                return balance;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<bool> CreateInvestment(Investment investment)
        {
            try
            {
                await _investmentRepository.Insert(investment);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<Balance> UpdateInvestmentBalance(Balance balance)
        {
            try
            {
                await _investmentBalanceRepository.Update(balance.Id, balance);

                return balance;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}