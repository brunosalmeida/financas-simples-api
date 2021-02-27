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
            Balance general = null;
            Balance investment = null;
            
            switch (request.Type)
            {
                case EBalanceType.Investment:
                     investment = await _investmentBalanceRepository.Get(request.UserId, request.AccountId);
                    break;
                case EBalanceType.Balance:
                    general = await _balanceRepository.Get(request.UserId, request.AccountId);
                    break;
                default:
                    general = await _balanceRepository.Get(request.UserId, request.AccountId);
                    investment = await _investmentBalanceRepository.Get(request.UserId, request.AccountId);
                    break;
            }
          
            return new GetBalanceResponse
            {
                AccountId =request.AccountId, 
                UserId = request.UserId,
                Balances = new Balances
                {
                    MovementBalance = general?.Value ?? 0,
                    MovementInvestment = investment?.Value ?? 0
                }
            };
        }
    }
}