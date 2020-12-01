namespace FS.Api.Application.Queries.Query
{
    using System;
    using DataObject.User.Responses;
    using MediatR;

    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }
}