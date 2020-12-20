namespace FS.Api.Application.Queries.Query
{
    using DataObject.Balance;
    using MediatR;
    using System;

    public class GetBalanceQuery : IRequest<GetBalanceResponse>
    {
        public Guid UserId { get; }
        public Guid AccountId { get; }

        public GetBalanceQuery(Guid userId, Guid accountId)
        {
            AccountId = accountId;
            UserId = userId;
        }
    }
}