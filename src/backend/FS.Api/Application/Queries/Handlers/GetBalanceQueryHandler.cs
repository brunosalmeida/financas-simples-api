// unset

namespace FS.Api.Application.Queries.Handlers
{
    using DataObject.Balance;
    using Domain.Core.Interfaces;
    using Domain.Model;
    using MediatR;
    using Query;
    using System.Threading;
    using System.Threading.Tasks;
    using Utils.Enums;

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, GetBalanceResponse>
    {
        private readonly IBalanceRepository _balanceRepository;
        private readonly IInvestmentBalanceRepository _investmentBalanceRepository;
        
        public GetBalanceQueryHandler(IBalanceRepository balanceRepository, 
            IInvestmentBalanceRepository investmentBalanceRepository)
        {
            _balanceRepository = balanceRepository;
            _investmentBalanceRepository = investmentBalanceRepository;
        }
        
        public async Task<GetBalanceResponse> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            Balance balance = request.Type switch
            {
                EBalanceType.Investment => await _investmentBalanceRepository.Get(request.UserId, request.AccountId),
                _ => await _balanceRepository.Get(request.UserId, request.AccountId)
            };

            if (balance is null)
                return null;

            return new GetBalanceResponse
            {
                Id = balance.Id, AccountId = balance.AccountId, UserId = balance.UserId, Balance = balance.Value
            };
        }
    }
}