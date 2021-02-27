namespace FS.Api.Application.Queries.Handlers
{
    using DataObject.Movement.Response;
    using Domain.Core.Interfaces;
    using MediatR;
    using Query;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllMovementsQueryHandler : IRequestHandler<GetAllMovementsQuery, IEnumerable<GetAllMovementResponse>>
    {
        private readonly IMovementRepository _movementRepository;

        public GetAllMovementsQueryHandler(IMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }

        public async Task<IEnumerable<GetAllMovementResponse>> Handle(GetAllMovementsQuery request,
            CancellationToken cancellationToken)
        {
            var movements = await _movementRepository.GetMovementsByAccount(request.UserId,
                request.AccountId, request.Page, request.PageSize);

            if (movements is null || !movements.Any())
                return Enumerable.Empty<GetAllMovementResponse>();

            return movements.Select(m => new GetAllMovementResponse
            {
                Id = m.Id,
                Value = m.Value,
                Description = m.Description,
                Category = m.Category,
                Type = m.Type,
                CreatedOn = m.CreatedOn
            });
        }
    }
}