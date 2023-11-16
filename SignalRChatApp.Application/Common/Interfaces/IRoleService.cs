using SignalRChatApp.Application.Common.Models;

namespace SignalRChatApp.Application.Common.Interfaces
{
    public interface IRoleService
    {
        Task<Response<object>> CreateRoleAsync(object model);

        Task<Response<object>> AssignRoles(string id, string roleName);
    }
}
