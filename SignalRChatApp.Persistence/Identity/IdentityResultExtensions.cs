using Microsoft.AspNetCore.Identity;
using SignalRChatApp.Application.Common.Models;

namespace SignalRChatApp.Persistence.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(x=>x.Description));
        }
    }
}
