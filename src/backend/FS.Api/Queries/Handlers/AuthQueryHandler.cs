using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FS.Api.Queries.Request;
using FS.Domain.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FS.Api.Queries.Handlers
{
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
            return id == Guid.Empty ? string.Empty : CreateToken(id);
        }
        
        private string CreateToken(Guid userId)
        {
            var claims = new[] {new Claim("user", userId.ToString())};
            
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials,
                claims:claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
        
    }
}