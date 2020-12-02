namespace FS.Api.Application.Queries.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Core.Interfaces;
    using MediatR;
    using Query;

    public class GetAccountByUserIdQueryHandler : IRequestHandler<GetAccountByUserIdQuery, Guid?>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountByUserIdQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Guid?> Handle(GetAccountByUserIdQuery request, CancellationToken cancellationToken)
        {
           var account = await _accountRepository.GetAccountByUserId(request.UserId);

           return account?.Id;
        }
    }
}