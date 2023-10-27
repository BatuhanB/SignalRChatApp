using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Application.Common.Interfaces;
using SignalRChatApp.Domain.Entities.Dtos;

namespace SignalRChatApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationDto model)
        {
            var result =  await _identityService.CreateUserAsync(model);
            if (result.result.Errors.Any())
            {
                return BadRequest(result.result.Errors);
            }
            else
            {
                return Ok(result);
            }
        }

    }
}
