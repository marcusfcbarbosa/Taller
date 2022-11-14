using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Taller.Api.Data;
using Taller.Core.Identity;

namespace Taller.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            //services.AddScoped<IRequestHandler<RegistrarClientCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddScoped<TallerDBContext>();

        }
    }
}
