using Microsoft.Extensions.DependencyInjection;
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
            return services;
        }
    }
}
