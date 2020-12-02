using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FS.Api.Helpers
{
    using System.Collections.Generic;

    public static class JwtHelper
    {
        public static string CreateToken(Dictionary<string, Guid> claims, string issuer, string audience, string key)
        {
            var _claims = new List<Claim>();

            foreach ((string claimName, Guid claimValue) in claims)
            {
                _claims.Add(new Claim(claimName, claimValue.ToString()));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials,
                claims: _claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}