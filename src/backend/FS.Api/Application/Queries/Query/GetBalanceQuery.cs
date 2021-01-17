namespace FS.Api.Application.Queries.Query
{
    using DataObject.Balance;
    using MediatR;
    using System;
    using Utils.Enums;

    public class GetBalanceQuery : IRequest<GetBalanceResponse>
    {
        public Guid UserId { get; }
        public Guid AccountId { get; }

        public EBalanceType Type { get; }

        public GetBalanceQuery(Guid userId, Guid accountId, EBalanceType type)
        {
            AccountId = accountId;
            Type = type;
            UserId = userId;
        }
    }
}