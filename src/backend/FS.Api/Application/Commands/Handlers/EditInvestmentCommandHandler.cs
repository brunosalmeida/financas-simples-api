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

    public class EditInvestmentCommandHandler : IRequestHandler<EditInvestmentCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IInvestmentService _investmentService;

        public EditInvestmentCommandHandler(IUserRepository repository, IAccountRepository accountRepository,
            IInvestmentService investmentService)
        {
            _userRepository = repository;
            _accountRepository = accountRepository;
            _investmentService = investmentService;
        }

        public async Task<Guid> Handle(EditInvestmentCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.Get(command.UserId) is var user && user is null)
                throw new Exception("Invalid user");

            if (await _accountRepository.Get(command.AccountId) is var account && account is null)
                throw new Exception("Invalid account");

            var balance = await _investmentService.UpdateInvestmentAndUpdateBalance(command.UserId, command.AccountId,
                command.InvestmentId, command.Value, command.Description, command.Type);

            return balance.Id;
        }
    }
}