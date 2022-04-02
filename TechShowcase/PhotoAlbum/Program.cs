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
    .ConfigureServices(services =>
    {
        services.AddHttpClient("PhotoApi", client =>
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        })
           .AddTransientHttpErrorPolicy(x =>
              x.WaitAndRetryAsync(RetryTimes, _ => TimeSpan.FromMilliseconds(WaitTimeForRetryInMilliseconds)));

        services.AddSingleton<IAlbumService, AlbumService>();
        services.AddSingleton<IConsoleService, ConsoleService>();
        services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        services.AddSingleton<IAlbumRepo, AlbumRepo>();

        services.AddHostedService<Application>();
    });