using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Taller.Core.Identity;
using Taller.Web.Services;

namespace Taller.Web.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
        }
    }
}
