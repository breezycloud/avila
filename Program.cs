using AvilaNaturalle;
using AvilaNaturalle.Data;
using AvilaNaturalle.Handlers;
using AvilaNaturalle.Shared.Models;
using AvilaNaturalle.Shared.Util;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Nosthy.Blazor.DexieWrapper.JsModule;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddSingleton<AppState>();
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddTransient<IConverter, Converter>();
builder.Services.AddScoped<IUserPreferencesService, UserPreferencesService>();

builder.Services.AddScoped<IModuleFactory, EsModuleFactory>();
builder.Services.AddScoped<MyDb>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
