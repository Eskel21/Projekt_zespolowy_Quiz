using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace Projekt_zespo³owy_Quiz
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
            services.AddRazorPages();  // Rejestrujemy obs³ugê stron Razor Pages

            // Rejestrujemy HttpClient z bazowym adresem API
            services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://quizapi.io");
            });

            // Mo¿esz dodaæ dodatkowe konfiguracje us³ug, jeœli s¹ potrzebne
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  // Wyj¹tkowa strona w trybie deweloperskim
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");  // Obs³uga b³êdów w przypadku innego œrodowiska
                app.UseHsts();  // W³¹cz HSTS (HTTP Strict Transport Security)
            }

            app.UseHttpsRedirection();  // Przekierowanie na protokó³ HTTPS
            app.UseStaticFiles();  // Obs³uga statycznych plików (CSS, JS, obrazy itp.)

            app.UseRouting();  // Konfiguracja routingu

            app.UseAuthorization();  // Obs³uga autoryzacji

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();// Mapowanie endpointów dla stron Razor Pages
                
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();  // Punkt wejœcia do aplikacji, który tworzy host i uruchamia aplikacjê.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)  // Tworzy domyœlny host ASP.NET Core.
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();  // Konfiguracja startowa aplikacji, wskazuje klasê Startup do inicjalizacji.
                });
    }
}
