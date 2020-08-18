using System.Threading;
using System.Threading.Tasks;
using FS.Api.Queries.Request;
using FS.DataObject.User.Responses;
using FS.Domain.Core.Interfaces;
using MediatR;

namespace FS.Api.Queries.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await this._userRepository.Get(request.Id);
            
            return new GetUserResponse(user.Id, user.Name, user.Email, user.Password);
        }
    }
}