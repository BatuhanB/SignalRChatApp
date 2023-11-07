using SignalRChatApp.Application.Common.Models;

namespace SignalRChatApp.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Response<object> result, string userId)> CreateUserAsync(object user);

        Task<Response<string>> DeleteUserAsync(string userId);

        Task<Response<object>> LoginAsync(object user);

        Task<Response<object>> LogoutAsync();

        Task<Response<object>> CreateRoleAsync(object model);

        Task<Response<object>> AssignRoles(string id);
    }
}
