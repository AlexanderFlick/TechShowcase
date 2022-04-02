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
            _console.PromptNextAlbum();
            var input = _console.GetAlbumIdFromInput();
            var album = _album.ById(input);
            _console.GiveAlbumOverview(album);
            _console.WriteAlbumAndPhotoInfo(album);
            finished = _console.CheckIfUserIsFinished();
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}