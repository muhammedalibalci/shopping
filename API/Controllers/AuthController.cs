using Domain.Dto;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] User user)
        {
            var state = await _authService.Register(user);
            if (state.HasError)
            {
                return BadRequest(state);
            }
            return Ok(state);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] AuthDto auth)
        {
            var result = await _authService.Login(auth);
            if (result.HasError)
            {
                return BadRequest(result);
            }
            return Ok(result.Data);
        }

        [HttpGet("[action]")]
        public async Task<Token> RefreshTokenLogin([FromForm] string refreshToken)
        {
            return await _authService.RefreshTokenLoginAsync(refreshToken);
        }

    }
}
