using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DogBreedsAPI.AuthorizationAuthentication
{
    public class GenerateToken
    {
        private readonly TokenConfiguration _configuration;
        public GenerateToken(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwt(AuthenticateLogin login)
        {
            var loginConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var usernameValidation = loginConfig.GetValue<string>("Username");
            var passwordValidation = loginConfig.GetValue<string>("Password");

            if (login.Username.Equals(usernameValidation) && login.Password.Equals(passwordValidation))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.Secret));
                var tokenHandler = new JwtSecurityTokenHandler();

                var nameClaim = new Claim("sub","DogBreedsAPI");
                var moduleClaim = new Claim("Module", "Web III.net");
                List<Claim> claims = new List<Claim>();
                claims.Add(nameClaim);
                claims.Add(moduleClaim);

                var jwtToken = new JwtSecurityToken(
                    issuer: _configuration.Issuer,
                    audience: _configuration.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(_configuration.ExpirationtimeInHours),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature));

                return tokenHandler.WriteToken(jwtToken);
            }
            return null;
        }
    }
}
