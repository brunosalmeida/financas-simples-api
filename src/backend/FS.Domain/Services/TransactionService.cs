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
        private readonly IMovementRepository _movementRepository;

        public TransactionService(IBalanceRepository balanceRepository, IMovementRepository movementRepository)
        {
            _balanceRepository = balanceRepository;
            _movementRepository = movementRepository;
        }

        public async Task<Balance> CreateOrUpdateBalance(Movement movement)
        {
            try
            {
                await CreateMovement(movement);
                var userBalance = await _balanceRepository.Get(movement.UserId, movement.AccountId);
                
                if (userBalance is null)
                {
                    return await FirstBalance(movement.UserId, movement.AccountId, movement.Value);
                }

                userBalance.UpdateBalance(movement.Value);
                await UpdateBalance(userBalance);

                return userBalance;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public async Task<Balance> UpdateMovementAndUpdateBalance(Guid userId, Guid accountId, Guid movementId,
            decimal value, string description = null)
        {
            var movement = await _movementRepository.Get(movementId);
            var userBalance = await _balanceRepository.Get(userId, accountId);

            var valueToRemove = movement.Value * -1;

            movement.SetValue(value);
            
            if(!string.IsNullOrEmpty(description))
                movement.SetDescription(description);
            
            movement.SetUpdateDate();

            await _movementRepository.Update(movementId, movement);

            userBalance.UpdateBalance(valueToRemove);
            userBalance.UpdateBalance(movement.Value);
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

        private async Task<bool> CreateMovement(Movement movement)
        {
            try
            {
                await _movementRepository.Insert(movement);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}