namespace FS.Api.Application.Commands.Handlers
{
    using Command;
    using Domain.Core.Interfaces;
    using Domain.Core.Interfaces.Services;
    using Domain.Model;
    using Domain.Model.Validators;
    using Hangfire;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateInstallmentMovementCommandHandler : IRequestHandler<CreateInstallmentMovementCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IInstallmentMovementRepository _installmentMovementRepository;

        public CreateInstallmentMovementCommandHandler(IUserRepository repository, IAccountRepository accountRepository,
            ITransactionService transactionService, IBackgroundJobClient backgroundJobClient,
            IInstallmentMovementRepository installmentMovementRepository)
        {
            _userRepository = repository;
            _accountRepository = accountRepository;
            _transactionService = transactionService;
            _backgroundJobClient = backgroundJobClient;
            _installmentMovementRepository = installmentMovementRepository;
        }

        public async Task<Guid> Handle(CreateInstallmentMovementCommand command,
            CancellationToken cancellationToken)
        {
            if (await _userRepository.Get(command.UserId) is var user && user is null)
                throw new Exception("Invalid user");

            if (await _accountRepository.Get(command.AccountId) is var account && account is null)
                throw new Exception("Invalid account");

            var installmentMovement = new InstallmentMovement(command.Value, command.Months, command.StartMonth, command.Description,
                command.Category, command.Type, command.AccountId,command.UserId);

            var installmentMovementValidator = new InstallmentMovementValidator();
            var result = await installmentMovementValidator.ValidateAsync(installmentMovement, default);

            if (!result.IsValid) throw new Exception(String.Join(" \n ", result.Errors));

            await _installmentMovementRepository.Insert(installmentMovement);
            
            var futureMovements = SplitIntoMovements(installmentMovement);
            await this.ScheduleFutureMovements(futureMovements);
            
            return installmentMovement.Id;
        }

        private List<Movement> SplitIntoMovements(InstallmentMovement installmentMovement)
        {
            var movements = new List<Movement>();
            var number = 1;
            
            for (int month = (int)installmentMovement.StartMonth; month < installmentMovement.EndMonth ; month++)
            {
                var description = $"({number}/{installmentMovement.Months})-Total:{installmentMovement.Value}-{installmentMovement.Description}";
                var futureDate = installmentMovement.CreatedOn.AddMonths(month);
                
                var movement = new Movement(installmentMovement.InstallmentsValue, description, installmentMovement.Category,
                    installmentMovement.Type, installmentMovement.AccountId, installmentMovement.UserId);
                movement.OverrideCreatedDate(futureDate);

                movements.Add(movement);

                number++;
            }
            
            return movements;
        }

        private async Task ScheduleFutureMovements(IList<Movement> futureMovements)
        {   
            foreach (var movement in futureMovements)
            {
                _backgroundJobClient.Schedule( () => _transactionService.CreateOrUpdateBalance(movement),
                    new DateTimeOffset(movement.CreatedOn).UtcDateTime);
            }

            await Task.CompletedTask;
        }
        
    }
}