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

    public class CreateInstallmentMovimentCommandHandler : IRequestHandler<CreateInstallmentMovimentCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public CreateInstallmentMovimentCommandHandler(IUserRepository repository, IAccountRepository accountRepository,
            ITransactionService transactionService, IBackgroundJobClient backgroundJobClient)
        {
            _userRepository = repository;
            _accountRepository = accountRepository;
            _transactionService = transactionService;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Guid> Handle(CreateInstallmentMovimentCommand command,
            CancellationToken cancellationToken)
        {
            if (await _userRepository.Get(command.UserId) is var user && user is null)
                throw new Exception("Invalid user");

            if (await _accountRepository.Get(command.AccountId) is var account && account is null)
                throw new Exception("Invalid account");

            var installmentMoviment = new InstallmentMoviment(command.Value, command.Months, command.StartMonth, command.Description,
                command.Category, command.Type, command.AccountId,command.UserId);

            var installmentMovimentValidator = new InstallmentMovimentValidator();
            var result = await installmentMovimentValidator.ValidateAsync(installmentMoviment, default);

            if (!result.IsValid) throw new Exception(String.Join(" \n ", result.Errors));

            var futureMoviments = SplitIntoMoviments(installmentMoviment);
            
            //var balance = await _transactionService.CreateOrUpdateBalance(futureMoviments[0]);
            await this.ScheduleFutureMoviments(futureMoviments);
            
            return installmentMoviment.Id;
        }

        private List<Moviment> SplitIntoMoviments(InstallmentMoviment installmentMoviment)
        {
            var moviments = new List<Moviment>();
            var number = 1;
            
            for (int month = installmentMoviment.StartMonth; month < installmentMoviment.EndMonth ; month++)
            {
                var description = $"({number}/{installmentMoviment.Months})-Total:{installmentMoviment.Value}-{installmentMoviment.Description}";
                var futureDate = new DateTime(installmentMoviment.CreatedOn.Year, month, installmentMoviment.CreatedOn.Day);
                
                var moviment = new Moviment(installmentMoviment.InstallmentsValue, description, installmentMoviment.Category,
                    installmentMoviment.Type, installmentMoviment.AccountId, installmentMoviment.UserId);
                moviment.OverrideCreatedDate(futureDate);

                moviments.Add(moviment);

                number++;
            }
            
            return moviments;
        }

        private async Task ScheduleFutureMoviments(IList<Moviment> futureMoviments)
        {
            var now = DateTime.Now;
            var index = 1;
            
            foreach (var moviment in futureMoviments)
            {
                _backgroundJobClient.Schedule( () => _transactionService.CreateOrUpdateBalance(moviment),
                    new DateTimeOffset(now.AddMinutes(index++)).UtcDateTime);
            }
        }
    }
}