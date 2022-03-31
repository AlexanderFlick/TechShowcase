using PhotoAlbum.Data;
using PhotoAlbum.Data.Models;

namespace PhotoAlbum.Services;
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
