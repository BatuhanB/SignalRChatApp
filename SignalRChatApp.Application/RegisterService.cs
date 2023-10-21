using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SignalRChatApp.Application
{
    public static class RegisterService
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // MediatR can read all the related files from Assembly
            services.AddMediatR(_ => _.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
