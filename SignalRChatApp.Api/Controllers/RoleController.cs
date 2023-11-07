using Microsoft.AspNetCore.Mvc;
using SignalRChatApp.Application.Common.Interfaces;
using SignalRChatApp.Application.Common.Models;
using SignalRChatApp.Domain.Entities.Dtos;

namespace SignalRChatApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public RoleController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<Response<object>> Create([FromBody] CreateRoleDto model)
        {
            return await _identityService.CreateRoleAsync(model);
        }

        [HttpPost]
        public async Task<Response<object>> Assign([FromHeader] string id)
        {
            return await _identityService.AssignRoles(id);
        }
    }
}
