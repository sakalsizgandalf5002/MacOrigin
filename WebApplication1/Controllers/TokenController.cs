using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService) => _tokenService = tokenService;

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthDto>> Refresh([FromBody] TokenRequest request)
        {
            var auth = await _tokenService.RefreshAsync(request.Token);
            return Ok(auth);
        }
    }
}

namespace WebApplication1.DTOs
{
    public record TokenRequest(Guid Token);
}