namespace FS.Api.Application.Queries.Query
{
    using System;
    using MediatR;

    public class GetAccountByUserIdQuery : IRequest<Guid?>
    {
        public GetAccountByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; private set; }
    }
}