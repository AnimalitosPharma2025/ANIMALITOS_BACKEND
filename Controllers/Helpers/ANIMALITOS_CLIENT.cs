using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ANIMALITOS_PHARMA_API.Custom
{
    public class ANIMALITOS_CLIENT
    {
        public IConfiguration _configuration;

        public ANIMALITOS_CLIENT(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User model)
        {
            var userClaims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
            new Claim(ClaimTypes.Email, model.Username)
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtConfig = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                userClaims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}