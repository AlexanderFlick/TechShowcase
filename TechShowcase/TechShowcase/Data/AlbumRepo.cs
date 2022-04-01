using PhotoAlbum.Data.Models;
using System.Text.Json;

namespace PhotoAlbum.Data;
public interface IAlbumRepo
{
    Album ById(int id);
}
public class AlbumRepo : IAlbumRepo
{
    public Album ById(int id)
    {
        using var client = new HttpClient();
        var uri = new Uri("https://jsonplaceholder.typicode.com/photos?albumId=" + id);
        var response = client.GetAsync(uri).Result;

        if(response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var albumResponse = JsonSerializer.Deserialize<List<AlbumApiResponse>>(content);
            var validAlbum = BuildAlbumFromResponse(albumResponse);
            return validAlbum;
        }

        return new Album();
    }

    public static Uri UriBuilder(int id) => new("https://jsonplaceholder.typicode.com/photos?albumId=" + id);

    public static Album BuildAlbumFromResponse(IEnumerable<AlbumApiResponse> albumResponse)
    {
        var response = albumResponse.ToList();
        var album = new Album
        {
            Id = response[0].Id,
            Photos = new List<Photo>()
        };

        response.ForEach(x =>
        {
            var photo = new Photo
            {
                Id = x.Id,
                Title = x.Title,
                Url = x.Url,
                ThumbnailUrl = x.ThumbnailUrl
            };

            album.Photos.Add(photo);
        });

        return album;
    }
}
