using PhotoAlbum.Data.Models;

namespace PhotoAlbum.Data.Extensions;

public static class AlbumExtensions
{
    public static Album Build(this Album album, IEnumerable<AlbumApiResponse> apiResponse)
    {
        album.Id = apiResponse.First().AlbumId;

        apiResponse.ToList().ForEach(x =>
        {
            var photo = new Photo(x.Id, x.Title, x.Url, x.ThumbnailUrl);
            album.Photos.Add(photo);
        });

        return album;
    }

    public static string Header(this Album album)
    {
        return $"[Album Id: {album.Id}]";
    }
}