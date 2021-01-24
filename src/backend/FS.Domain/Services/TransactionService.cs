namespace FS.Domain.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Interfaces;
    using Interfaces.Services;
    using Model;
    using Utils.Enums;

    public class TransactionService : ITransactionService
    {
        private readonly IBalanceRepository _balanceRepository;
        private readonly IMovimentRepository _movimentRepository;

        public TransactionService(IBalanceRepository balanceRepository, IMovimentRepository movimentRepository)
        {
            _balanceRepository = balanceRepository;
            _movimentRepository = movimentRepository;
        }

        public async Task<Balance> CreateOrUpdateBalance(Moviment moviment)
        {
            try
            {
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
        
        public async Task<Balance> UpdateMovimentAndUpdateBalance(Guid userId, Guid accountId, Guid movimentId,
            decimal value, string description = null)
        {
            var moviment = await _movimentRepository.Get(movimentId);
            var userBalance = await _balanceRepository.Get(userId, accountId);

            var valueToRemove = moviment.Value * -1;

            moviment.SetValue(value);
            
            if(!string.IsNullOrEmpty(description))
                moviment.SetDescription(description);
            
            moviment.SetUpdateDate();

            await _movimentRepository.Update(movimentId, moviment);

            userBalance.UpdateBalance(valueToRemove);
            userBalance.UpdateBalance(moviment.Value);
            userBalance.SetUpdateDate();

            await UpdateBalance(userBalance);

            return userBalance;
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