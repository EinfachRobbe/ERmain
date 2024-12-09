using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ERmain;
using ERmain.Data.Language;
using ERmain.Data.Theme;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMemoryCache();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://cms.einfachrobbe.de/") });
builder.Services.AddScoped<LanguageAPI>();
builder.Services.AddScoped<LanguageService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));

await builder.Build().RunAsync();