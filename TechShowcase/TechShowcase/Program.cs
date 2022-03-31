using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechShowcase;
using TechShowcase.Data;
using TechShowcase.Services;
using TechShowcase.Wrappers;

CreateHostBuilder(args).Build().RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IAlbumService, AlbumService>();
        services.AddSingleton<IConsoleService, ConsoleService>();
        services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        services.AddSingleton<IAlbumRepo, AlbumRepo>();

        services.AddHostedService<Application>();
    });