using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoAlbum;
using PhotoAlbum.Data;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;

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