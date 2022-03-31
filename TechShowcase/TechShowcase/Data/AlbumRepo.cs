using PhotoAlbum.Data.Models;

namespace PhotoAlbum.Data;
public interface IAlbumRepo
{
    Album ById(int id);
}
public class AlbumRepo : IAlbumRepo
{
    public Album ById(int id)
    {
        throw new NotImplementedException();
    }
}
