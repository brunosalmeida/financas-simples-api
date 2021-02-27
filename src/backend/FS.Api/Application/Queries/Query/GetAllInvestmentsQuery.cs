namespace FS.Api.Application.Queries.Query
{
    using DataObject.Movement.Response;
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class GetAllInvestmentsQuery : BaseGetAllInvestmentQuery, IRequest<IEnumerable<GetAllInvestmentResponse>>
    {
        public GetAllInvestmentsQuery(Guid userId, Guid accountId, int page, int pageSize)
            : base(accountId, userId)
        {
            PageSize = pageSize;
            Page = page;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}