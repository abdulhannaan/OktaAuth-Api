using Microsoft.Extensions.DependencyInjection;
using Okta.Services.Logging;
using Okta.Services.User;
using OktaAuth.Helper;

namespace Okta.RegisterServices
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<OktaApiHelper>();
            services.AddSingleton<ILoggerManager, LoggerManager>();

            return services;
        }
    }
}
