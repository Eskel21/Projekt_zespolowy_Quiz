using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Quizy.Data;
using System;
using System.Net.Http;

namespace Projekt_Quizy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext and Identity
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();  // Register Razor Pages support

            // Register HttpClient with the base API address
            services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://quizapi.io");
            });

            // You can add additional service configurations if needed
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  // Exception page in development mode
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");  // Error handling for other environments
                app.UseHsts();  // Enable HSTS (HTTP Strict Transport Security)
            }

            app.UseHttpsRedirection();  // Redirect to HTTPS
            app.UseStaticFiles();  // Serve static files (CSS, JS, images, etc.)

            app.UseRouting();  // Configure routing

            app.UseAuthentication();  // Enable authentication
            app.UseAuthorization();  // Authorization handling

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();  // Map endpoints for Razor Pages
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();  // Entry point that creates a host and runs the application.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)  // Create the default ASP.NET Core host.
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();  // Set the startup configuration to use the Startup class.
                });
    }
}
