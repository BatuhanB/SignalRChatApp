using Microsoft.AspNetCore.Identity;

namespace SignalRChatApp.Persistence.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public required string FirstName{ get; set; }
        public required string LastName { get; set; }
    }
}
