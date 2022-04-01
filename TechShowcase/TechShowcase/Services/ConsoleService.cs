using PhotoAlbum.Data.Models;
using PhotoAlbum.Wrappers;

namespace PhotoAlbum.Services;

public interface IConsoleService
{
    void Greeting();

    int GetAlbumIdFromInput();

    void WritePhotoInfoFromAlbum(Album album);

    bool CheckIfUserIsFinished();
}

public class ConsoleService : IConsoleService
{
    private readonly IConsoleWrapper _console;

    public ConsoleService(IConsoleWrapper console)
    {
        _console = console;
    }

    public void Greeting() => _console.Write("Welcome! Give me the number of the photo album that you want to view.");

    public static string PhotoInfo(Photo photo) => $"Photo Id: {photo.Id}\nPhoto Title: {photo.Title}\n--------------------";

    public int GetAlbumIdFromInput()
    {
        var response = _console.Read();
        var validInput = int.TryParse(response, out var albumId);
        return validInput ? albumId : 0;
    }

    public void WritePhotoInfoFromAlbum(Album album)
    {
        _console.Write($"[Album Id: {album.Id}]");
        album.Photos.ForEach(photo => _console.Write(PhotoInfo(photo)));
    }

    public bool CheckIfUserIsFinished()
    {
        _console.Write("Are you finished? \nIf you type another album number, you can see your other photos");
        var response = _console.Read();
        if (response == "2")
        {
            return false;
        }
        return true;
    }
}