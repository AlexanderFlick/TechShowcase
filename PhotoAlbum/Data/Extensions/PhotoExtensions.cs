using PhotoAlbum.Data.Models;

namespace PhotoAlbum.Data.Extensions;

public static class PhotoExtensions
{
    public static string Info(this Photo photo)
    {
        return $"--------------------\nPhoto Id: {photo.Id}\nPhoto Title: {photo.Title}";
    }
}