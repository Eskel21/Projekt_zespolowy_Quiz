using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace Projekt_zespo�owy_Quiz
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
            services.AddRazorPages();  // Rejestrujemy obs�ug� stron Razor Pages

            // Rejestrujemy HttpClient z bazowym adresem API
            services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://quizapi.io");
            });

            // Mo�esz doda� dodatkowe konfiguracje us�ug, je�li s� potrzebne
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  // Wyj�tkowa strona w trybie deweloperskim
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");  // Obs�uga b��d�w w przypadku innego �rodowiska
                app.UseHsts();  // W��cz HSTS (HTTP Strict Transport Security)
            }

            app.UseHttpsRedirection();  // Przekierowanie na protok� HTTPS
            app.UseStaticFiles();  // Obs�uga statycznych plik�w (CSS, JS, obrazy itp.)

            app.UseRouting();  // Konfiguracja routingu

            app.UseAuthorization();  // Obs�uga autoryzacji

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();// Mapowanie endpoint�w dla stron Razor Pages
                
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();  // Punkt wej�cia do aplikacji, kt�ry tworzy host i uruchamia aplikacj�.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)  // Tworzy domy�lny host ASP.NET Core.
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();  // Konfiguracja startowa aplikacji, wskazuje klas� Startup do inicjalizacji.
                });
    }
}
