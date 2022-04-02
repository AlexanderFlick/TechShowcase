using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoAlbum;
using PhotoAlbum.Data;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;
using Polly;

const int RetryTimes = 2;
const int WaitTimeForRetryInMilliseconds = 500;

CreateHostBuilder(args).Build().RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, app) =>
    {
        app.AddJsonFile("appsettings.json", true);
    })
    .ConfigureServices(services =>
    {
        var serviceProvider = services.BuildServiceProvider();
        var config = serviceProvider.GetService<IConfiguration>();

        services.AddSingleton<IAlbumService, AlbumService>();
        services.AddSingleton<IConsoleService, ConsoleService>();
        services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        services.AddSingleton<IAlbumRepo, AlbumRepo>();

        services.AddHttpClient("photoAlbumApi", client =>
        {
            client.BaseAddress = new Uri(config.GetConnectionString("photoAlbumApi"));
        })
        .AddTransientHttpErrorPolicy(x =>
            x.WaitAndRetryAsync(RetryTimes, _ => TimeSpan.FromMilliseconds(WaitTimeForRetryInMilliseconds)));

        services.AddHostedService<Application>();
    });