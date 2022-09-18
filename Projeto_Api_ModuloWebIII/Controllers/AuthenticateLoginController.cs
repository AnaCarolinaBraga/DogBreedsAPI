using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DogBreedsAPI.AuthorizationAuthentication;
using DogBreedsAPI.Models;

namespace DogBreedsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateLoginController : ControllerBase
    {
        private readonly GenerateToken _generateToken;
        public AuthenticateLoginController(GenerateToken generateToken)
        {
            _generateToken = generateToken;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<DogBreeds>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] AuthenticateLogin authInfo)
        {
            var user = authInfo;

            var token = _generateToken.GenerateJwt(authInfo);
            if (token == null)
            {
                return NotFound(new { message = "Usuário ou senha Inválidos" });
            }
            user.Password = "";
            return Ok(new { user = user.Username, token = token });
        }
    }
}
