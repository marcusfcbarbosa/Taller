using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taller.Web.Extensions;
using Taller.Web.Middleware;

namespace Taller.Web.Configuration
{
    public static class WebAppConfig
    {
        public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddControllersWithViews();
            services.Configure<AppSettings>(configuration);
        }

        public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region ""
            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseHsts();
            #endregion
            app.UseWebSockets();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityConfiguration();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
