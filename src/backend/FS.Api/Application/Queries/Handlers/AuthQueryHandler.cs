namespace FS.Api.Application.Queries.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.Command;
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
        private readonly IMediator _mediator;

        public AuthQueryHandler( IConfiguration configuration, 
            IUserRepository userRepository, IMediator mediator)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<AuthenticationResponse> Handle(AuthUserQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetUserByUsernameAndPassword(
                request.Username, Utils.Helpers.PasswordHelper.Encrypt(request.Password));
            
            if(userId == Guid.Empty)
                return new AuthenticationResponse(new List<string>{"Usuário ou senha inválidos"});

            var accountId = await GetAccount(userId);

            var claims = new Dictionary<string, Guid> {{"user", userId}, {"account", accountId}};

            var token = JwtHelper.CreateToken (claims, _configuration["Jwt:Issuer"], 
                 _configuration["Jwt:Audience"],_configuration["Jwt:Key"]);
             
             return new AuthenticationResponse(token);
        }

        private async Task<Guid> GetAccount(Guid userId)
        {
            var accountId = await _mediator.Send(new GetAccountByUserIdQuery(userId)) ?? 
                            await _mediator.Send(new CreateAccountCommand(userId));

            return accountId;
        }
    }
}