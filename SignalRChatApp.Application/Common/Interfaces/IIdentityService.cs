using SignalRChatApp.Application.Common.Models;

namespace SignalRChatApp.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result result, string userId)> CreateUserAsync(object user, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
