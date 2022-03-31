using TechShowcase.Data;
using TechShowcase.Data.Models;

namespace TechShowcase.Services;
public interface IAlbumService
{
    Album ById(int id);
}
public class AlbumService : IAlbumService
{
    private readonly IAlbumRepo _album;

    public AlbumService(IAlbumRepo album)
    {
        _album = album;
    }
    public Album ById(int id)
    {
        throw new NotImplementedException();
    }
}
