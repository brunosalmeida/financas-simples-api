namespace FS.Domain.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Interfaces;
    using Interfaces.Services;
    using Model;

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
                var userBalance = await _balanceRepository.Get(moviment.UserId, moviment.AccountId);

                if (userBalance is null)
                {
                    await CreateMoviment(moviment);
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