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
        private readonly IInvestmentBalanceRepository _investmentBalanceRepository;
        private readonly IInvestmentRepository _investmentRepository;

        public InvestmentService(IBalanceRepository balanceRepository, IMovimentRepository movimentRepository, 
            IInvestmentBalanceRepository investmentBalanceRepository, IInvestmentRepository investmentRepository)
        {
            _balanceRepository = balanceRepository;
            _movimentRepository = movimentRepository;
            _investmentBalanceRepository = investmentBalanceRepository;
            _investmentRepository = investmentRepository;
        }


        public async Task<Balance> CreateOrUpdateBalance(Investment investment)
        {
            try
            {
                await CreateInvestment(investment);
                
                var userInvestmentBalance = await _balanceRepository.Get(investment.UserId, investment.AccountId);

                if (userInvestmentBalance is null)
                {
                    return await FirstInvestmentBalance(investment.UserId, investment.AccountId, investment.Value);
                }

                userInvestmentBalance.UpdateBalance(investment.Value);
                await UpdateInvestmentBalance(userInvestmentBalance);
                
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