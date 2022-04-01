using Microsoft.Extensions.Hosting;
using PhotoAlbum.Services;

namespace PhotoAlbum;

public class Application : IHostedService
{
    private readonly IConsoleService _console;
    private readonly IAlbumService _albums;

    public Application(IConsoleService console, IAlbumService albumService)
    {
        _console = console;
        _albums = albumService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _console.Greeting();
        var finished = false;
        while (!finished)
        {
            var input = _console.GetAlbumIdFromInput();
            var photos = _albums.ById(input);
            _console.WritePhotoInfoFromAlbum(photos);
            finished = _console.CheckIfUserIsFinished();
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}