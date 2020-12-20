// unset

namespace FS.Api.Application.Queries.Handlers
{
    using DataObject.Balance;
    using Domain.Core.Interfaces;
    using MediatR;
    using Query;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, GetBalanceResponse>
    {
        private readonly IBalanceRepository _balanceRepository;
        
        public GetBalanceQueryHandler(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }
        
        public async Task<GetBalanceResponse> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var balance = await _balanceRepository.Get(request.UserId, request.AccountId);

            if (balance is null)
                return null;

            return new GetBalanceResponse
            {
                Id = balance.Id, AccountId = balance.AccountId, UserId = balance.UserId, Balance = balance.Value
            };
        }
    }
}