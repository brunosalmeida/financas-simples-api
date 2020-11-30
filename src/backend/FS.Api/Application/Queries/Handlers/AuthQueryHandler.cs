namespace FS.Api.Application.Queries.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Core.Interfaces;
    using Helpers;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Query;

    public class AuthQueryHandler: IRequestHandler<AuthUserQuery, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthQueryHandler( IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(AuthUserQuery request, CancellationToken cancellationToken)
        {
            var id = await _userRepository.GetUserByUsernameAndPassword(request.Username, request.Password);
            return id == Guid.Empty ? string.Empty : JwtHelper.CreateToken (id, _configuration["Jwt:Issuer"], 
                _configuration["Jwt:Audience"],_configuration["Jwt:Key"]);
        }
    }
}