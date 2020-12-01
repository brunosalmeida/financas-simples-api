namespace FS.Api.Application.Queries.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DataObject.Authentication;
    using Domain.Core.Interfaces;
    using Helpers;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Query;

    public class AuthQueryHandler: IRequestHandler<AuthUserQuery, AuthenticationResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthQueryHandler( IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> Handle(AuthUserQuery request, CancellationToken cancellationToken)
        {
            var id = await _userRepository.GetUserByUsernameAndPassword(
                request.Username, Utils.Helpers.PasswordHelper.Encrypt(request.Password));
            
            if(id == Guid.Empty)
                return new AuthenticationResponse(new List<string>{"Usuário ou senha inválidos"});
                    
             var token = JwtHelper.CreateToken (id, _configuration["Jwt:Issuer"], 
                 _configuration["Jwt:Audience"],_configuration["Jwt:Key"]);
             
             return new AuthenticationResponse(token);
        }
    }
}