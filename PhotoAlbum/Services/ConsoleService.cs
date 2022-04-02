using PhotoAlbum.Data.Extensions;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Wrappers;

namespace PhotoAlbum.Services;

public interface IConsoleService
{
    void Greeting();

    void PromptNextAlbum();

    int GetAlbumIdFromInput();

    void WriteAlbumAndPhotoInfo(Album album);

    void GiveAlbumOverview(Album album);

    bool UserIsFinished();
}

public class ConsoleService : IConsoleService
{
    private readonly IConsoleWrapper _console;
    private bool _firstTime = true;

    public ConsoleService(IConsoleWrapper console)
    {
        _console = console;
    }

    public void Greeting()
    {
        _console.Write("Welcome! I heard you like photo albums. Give me an album number and see what happens...");
    }

    public void PromptNextAlbum()
    {
        if (!_firstTime)
        {
            _console.Write("Give me the number of the next photo album that you want to view.");
        }

        _firstTime = false;
    }

    public int GetAlbumIdFromInput()
    {
        var response = _console.Read();
        var validInput = int.TryParse(response, out var albumId);
        return validInput ? albumId : 0;
    }

    public void GiveAlbumOverview(Album album)
    {
        _console.Write($"Oh boy, album no. {album.Id}! I've heard scandalous things about that album." +
            $"\nYou have {album.Photos.Count} photos in that album, apparently. \nHit 'enter' and see what is inside. No tricks here, promise.");
        _console.Read();
    }

    public void WriteAlbumAndPhotoInfo(Album album)
    {
        _console.Write(album.Header());
        album.Photos.ForEach(photo => _console.Write(photo.Info()));
    }

    public bool UserIsFinished()
    {
        _console.Write("\nPhew! That was a lot of photos. Are you done looking at photo albums? \nPlease type 'n' if you are done, 'y' if you want to view another album.");
        var response = _console.Read();
        if (response == "n" || response == "N")
        {
            return true;
        }
        _console.Write("Oh, the adventurous type, I like that.");
        return false;
    }
}