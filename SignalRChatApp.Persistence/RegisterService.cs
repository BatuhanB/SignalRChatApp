using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalRChatApp.Application.Common.Interfaces;
using SignalRChatApp.Persistence.DbContext;
using SignalRChatApp.Persistence.Identity;
using System;

namespace SignalRChatApp.Persistence
{
    public static class RegisterService
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresqlConnection");
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddScoped<IIdentityService,IdentityService >();
            
        }
    }
}
