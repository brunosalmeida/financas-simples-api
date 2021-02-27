// unset

namespace FS.Api.Application.Queries.Query
{
    using System;

    public abstract class BaseGetAllMovementQuery
    {
        protected BaseGetAllMovementQuery(Guid accountId, Guid userId)
        {
            AccountId = accountId;
            UserId = userId;
        }

        public Guid UserId { get; }
        public Guid AccountId { get;  }
    }
}