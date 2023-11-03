using Microsoft.AspNetCore.Identity;

namespace SignalRChatApp.Persistence.Identity
{
    public abstract class UserRoles:IdentityRole<string>
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
    }
}
