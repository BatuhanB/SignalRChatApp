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
            var result = await _identityService.CreateUserAsync(model);
            return new ObjectResult(result)
            {
                StatusCode = result.result.StatusCode
            };
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var result = await _identityService.LoginAsync(model);
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var result = await _identityService.LogoutAsync();
            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }
    }
}
