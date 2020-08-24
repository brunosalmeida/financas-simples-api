using System;
using System.Threading;
using System.Threading.Tasks;
using FS.Api.Queries.Request;
using FS.Domain.Core.Interfaces;
using MediatR;

namespace FS.Api.Queries.Handlers
{
    public class AuthQueryHandler: IRequestHandler<AuthUserQuery, Guid>
    {
        private readonly IUserRepository _userRepository;

        public AuthQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(AuthUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByUsernameAndPassword(request.Username, request.Password);
        }
    }
}