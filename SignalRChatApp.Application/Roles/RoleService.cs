using Microsoft.AspNetCore.Identity;
using SignalRChatApp.Application.Common.Interfaces;
using SignalRChatApp.Application.Common.Models;
using SignalRChatApp.Domain.Entities.Dtos;
using SignalRChatApp.Persistence.Identity;

namespace SignalRChatApp.Application.Roles
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<UserRoles> _roleManager;

        public RoleService(UserManager<ApplicationUser> userManager,
            RoleManager<UserRoles> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response<object>> AssignRoles(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Response<object>.Success(new { Role = roleName, UserId = user.Id }, 200);
            }
            var err = result.Errors.Select(x => x.Description).ToList();
            var errors = new Error(err, false);
            return Response<object>.Fail(400, errors);
        }

        public async Task<Response<object>> CreateRoleAsync(object model)
        {
            var modelCast = (CreateRoleDto)model;
            var role = new UserRoles() { Id = Guid.NewGuid().ToString(), Name = modelCast.Name };
            var result = new IdentityResult();
            if (!string.IsNullOrEmpty(role.Name))
            {
                if (!(await _roleManager.RoleExistsAsync(role.Name)))
                {
                    result = await _roleManager.CreateAsync(role);
                }
            }
            if (result.Succeeded)
            {
                return Response<object>.Success(role, 200);
            }
            var errors = result.Errors.Select(x => x.Description).ToList();
            var err = new Error(errors, false);
            return Response<object>.Fail(400, err);
        }
    }
}
