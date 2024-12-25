using System.Reflection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ERmain;
using ERmain.Data.Language;
using ERmain.Data.Projects;
using ERmain.Data.Story;
using ERmain.Data.Theme;

ILogger<Program> logger;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Configure(builder.Services);
ConfigureCMS(builder.Services);

var app = builder.Build();

await app.RunAsync();

void Configure(IServiceCollection services)
{
    services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));
    logger = services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
    services.AddBlazoredLocalStorage();
    services.AddMemoryCache();
    services.AddScoped<HttpClient>();
}

void ConfigureCMS(IServiceCollection services)
{
    services.AddScoped<LanguageAPI>();
    services.AddScoped<LanguageService>();
    services.AddScoped<ThemeService>();
    services.AddScoped<StoryAPI>();
    services.AddScoped<ProjectsAPI>();
}


