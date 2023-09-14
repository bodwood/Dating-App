using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            }; 

            // Generate signing credentials with the key.
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Describe how the token will look.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // What claims the user has.
                Expires = DateTime.Now.AddDays(7), // How long the token will be valid for.
                SigningCredentials = creds // Signing credentials.
            };

            // New instance of the token handler.
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create token with the token handler and pass in the token descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the written token.
            return tokenHandler.WriteToken(token);
        }
    }
}