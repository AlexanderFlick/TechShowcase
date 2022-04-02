using PhotoAlbum.Data;
using PhotoAlbum.Data.Models;

namespace PhotoAlbum.Services;

public interface IAlbumService
{
    Task<Album> ById(int id);
}

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepo _album;

    public AlbumService(IAlbumRepo album)
    {
        _album = album;
    }

    public async Task<Album> ById(int id)
    {
        return await _album.ById(id);
    }
}