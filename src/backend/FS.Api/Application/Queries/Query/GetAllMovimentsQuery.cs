namespace FS.Api.Application.Queries.Query
{
    using DataObject.Moviment.Response;
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class GetAllMovimentsQuery : BaseGetAllMovimentQuery, IRequest<IEnumerable<GetAllMovimentResponse>>
    {
        public GetAllMovimentsQuery(Guid userId, Guid accountId, int page, int pageSize)
            : base(accountId, userId)
        {
            PageSize = pageSize;
            Page = page;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}