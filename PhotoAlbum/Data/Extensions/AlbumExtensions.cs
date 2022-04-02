using PhotoAlbum.Data.Models;

namespace PhotoAlbum.Data.Extensions;

public static class AlbumExtensions
{
    public static string Header(this Album album)
    {
        return $"[Album Id: {album.Id}]";
    }
}