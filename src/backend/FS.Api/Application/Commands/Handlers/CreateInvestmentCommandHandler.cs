namespace FS.Api.Application.Commands.Handlers
{
    using Command;
    using Domain.Core.Interfaces;
    using Domain.Core.Interfaces.Services;
    using Domain.Model;
    using Domain.Model.Validators;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Utils.Enums;

    public class CreateInvestmentCommandHandler : IRequestHandler<CreateInvestmentCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IInvestmentService _investmentService;

        public CreateInvestmentCommandHandler(IUserRepository repository, IAccountRepository accountRepository,
            IInvestmentService investmentService)
        {
            _userRepository = repository;
            _accountRepository = accountRepository;
            _investmentService = investmentService;
        }

        public async Task<Guid> Handle(CreateInvestmentCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.Get(command.UserId) is var user && user is null)
                throw new Exception("Invalid user");

            if (await _accountRepository.Get(command.AccountId) is var account && account is null)
                throw new Exception("Invalid account");

            var investment = new Investment(command.Value, command.Description, command.Type, command.AccountId,
                command.UserId);
            
            var investmentValidator = new InvestmentValidator();
            var result = await investmentValidator.ValidateAsync(investment, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));

            var balance = await _investmentService.CreateOrUpdateBalance(investment);

            return balance.Id;
        }
    }
}