using Microsoft.Extensions.Hosting;
using PhotoAlbum.Services;

namespace PhotoAlbum;

public class Application : IHostedService
{
    private readonly IConsoleService _console;
    private readonly IAlbumService _album;

    public Application(IConsoleService console, IAlbumService albumService)
    {
        _console = console;
        _album = albumService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _console.Greeting();

        var finished = false;
        while (!finished)
        {
            RunApplication();
            finished = _console.CheckIfUserIsFinished();
        }

        return Task.CompletedTask;
    }

    public void RunApplication()
    {
        _console.PromptNextAlbum();
        var input = _console.GetAlbumIdFromInput();
        var album = _album.ById(input).Result;
        _console.GiveAlbumOverview(album);
        _console.WriteAlbumAndPhotoInfo(album);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}