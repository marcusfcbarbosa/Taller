using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taller.Core.Identity;
using Taller.Identity.Api.Data;

namespace Taller.Identity.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TallerDBIdentityContext>(optionsAction: options
                  => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TallerDBIdentityContext>()
                .AddDefaultTokenProviders();
            services.AddJWTConfiguration(configuration);
            return services;
        }
    }
}
