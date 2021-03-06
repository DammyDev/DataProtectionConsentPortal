using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blazor.FileReader;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Server;
using POSPortal.Services;
using System;

namespace POSPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/AccessDenied";
                //options.Cookie.Expiration = TimeSpan.FromDays(60);
                //options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<POSService>();
            services.AddFileReaderService();

            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
                            // setting up policies
            services.AddAuthorization(options => {
                options.AddPolicy("MustBeAdmin", p => p.RequireAuthenticatedUser().RequireClaim("Role", "admin"));
                options.AddPolicy("MustBeBranch", p => p.RequireAuthenticatedUser().RequireClaim("Role", "branch"));
                options.AddPolicy("MustBeAdminOrUser", p => p.RequireAuthenticatedUser().RequireClaim("Role", "admin", "user"));
            });

            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
