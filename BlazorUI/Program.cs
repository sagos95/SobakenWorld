using BlazorUI;
using BlazorUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<Head>("head");
//builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<UiNotificationBus>();
builder.Services.AddSingleton<JsFunctions>();
builder.Services.AddScoped<GameFactory>();

await builder.Build().RunAsync();

