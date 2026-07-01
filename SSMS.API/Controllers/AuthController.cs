using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSMS.Application.Features.Auth.Commands;

namespace SSMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> ExchangeToken([FromBody] ExchangeCodeForTokenCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
