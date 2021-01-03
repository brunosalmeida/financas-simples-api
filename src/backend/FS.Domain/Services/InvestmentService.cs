// unset

namespace FS.Domain.Core.Services
{
    using Interfaces;
    using Interfaces.Services;
    using Model;
    using System;
    using System.ComponentModel.Design;
    using System.Threading.Tasks;
    using Utils.Enums;

    public class InvestmentService : IInvestmentService
    {
        private readonly IBalanceRepository _balanceRepository;
        private readonly IMovimentRepository _movimentRepository;

        public InvestmentService(IBalanceRepository balanceRepository, IMovimentRepository movimentRepository)
        {
            _balanceRepository = balanceRepository;
            _movimentRepository = movimentRepository;
        }


        public async Task<Balance> CreateOrUpdateBalance(Investment investment)
        {
            try
            {
                var moviment = new Moviment(investment.Value, investment.Description, EMovimentCategory.Investment,
                    EMovimentType.Investment, investment.AccountId, investment.UserId);

                await CreateMoviment(moviment);
                var userBalance = await _balanceRepository.Get(moviment.UserId, moviment.AccountId);

                if (userBalance is null)
                {
                    return await FirstBalance(moviment.UserId, moviment.AccountId, moviment.Value);
                }

                userBalance.UpdateBalance(moviment.Value);
                await UpdateBalance(userBalance);

                return userBalance;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<Balance> UpdateBalance(Balance balance)
        {
            try
            {
                await _balanceRepository.Update(balance.Id, balance);

                return balance;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<Balance> FirstBalance(Guid userId, Guid accountId, decimal value)
        {
            try
            {
                var balance = new Balance(userId, accountId, value);
                await _balanceRepository.Insert(balance);

                return balance;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<bool> CreateMoviment(Moviment moviment)
        {
            try
            {
                await _movimentRepository.Insert(moviment);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}