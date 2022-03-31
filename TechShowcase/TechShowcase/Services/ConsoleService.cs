using TechShowcase.Data.Models;
using TechShowcase.Wrappers;

namespace TechShowcase.Services;

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

    public int GetAlbumIdFromInput()
    {
        var response = _console.Read();
        var validInput = int.TryParse(response, out var albumId);
        return validInput ? albumId : 0;
    }

    public void WritePhotoInfoFromAlbum(Album album)
    {
        throw new NotImplementedException();
    }

    public bool CheckIfUserIsFinished()
    {
        throw new NotImplementedException();
    }


}
