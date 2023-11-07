using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Persistence.Identity;
using System.Reflection;

namespace SignalRChatApp.Persistence.DbContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,UserRoles,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
