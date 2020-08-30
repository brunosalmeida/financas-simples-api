using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FS.Api.Helpers
{
    public static class JwtHelper
    {
        public static string CreateToken(Guid userId, string issuer, string audience, string key)
        {
            var claims = new[] {new Claim("user", userId.ToString())};
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
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

        public static string GetClaim(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            
            return token.Claims.First(claim => claim.Type == "jti").Value;
        }
    }
}