using DynamicCssPocApp.Css;
using DynamicCssPocApp.Server.Services;
using DynamicCssPocApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ProofOfConcept
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<ThemeRepository>();
            builder.Services.AddScoped<CssService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                MapThemeEndpoints(endpoints);
            });

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToPage("/_Index");

            app.Run();

        }

        private static void MapThemeEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Theme", async (http) =>
            {
                var themeService = http.RequestServices.GetService<ThemeRepository>();
                var data = await http.Request.ReadFromJsonAsync<ThemeData>();
                await themeService!.AddNew(data!);
            });
            endpoints.MapGet("/Theme/{stringId}/boxstyle.css", async (http) =>
            {
                if (!http.Request.RouteValues.TryGetValue("stringId", out var id))
                {
                    http.Response.StatusCode = 400;
                    return;
                }

                var themeService = http.RequestServices.GetService<ThemeRepository>()!;
                var cssRendere = http.RequestServices.GetService<CssService>()!;

                var theme = await themeService.Get(id?.ToString());
                var css = await cssRendere.RenderCss(theme);
                await http.Response.WriteAsync(css);
            });
            endpoints.MapGet("/Theme/newest", async (http) =>
            {
                var themeService = http.RequestServices.GetService<ThemeRepository>()!;
                var id = themeService.NewestId();
                await http.Response.WriteAsync(id);
            });
        }
    }
}