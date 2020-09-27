using System;
using FS.DataObject.User.Responses;
using MediatR;

namespace FS.Api.Queries.Request
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }
}