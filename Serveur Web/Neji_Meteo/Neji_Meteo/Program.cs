using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Neji_Meteo.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Configurer le serveur pour �couter sur toutes les interfaces r�seau
builder.WebHost.UseUrls("http://0.0.0.0:5137");

// Ajouter les services n�cessaires
builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson();

builder.Services.AddRazorPages();
builder.Services.AddSignalR(); // Ajout de SignalR pour la mise � jour en temps r�el

// Activer les logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapHub<WeatherHub>("/weatherHub"); // Configuration du hub SignalR

app.MapGet("/", context =>
{
    context.Response.Redirect("/weather");
    return Task.CompletedTask;
});

app.Run();