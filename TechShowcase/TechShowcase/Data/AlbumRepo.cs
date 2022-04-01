using Newtonsoft.Json;
using PhotoAlbum.Data.Extensions;
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
        using var client = new HttpClient();
        var uri = UriBuilder(id);
        var response = client.GetAsync(uri).Result;

        if (response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var apiResponses = JsonConvert.DeserializeObject<List<AlbumApiResponse>>(content);
            return BuildAlbumFromApiResponse(apiResponses);
        }

        return new Album();
    }

    public static Uri UriBuilder(int id) => new("https://jsonplaceholder.typicode.com/photos?albumId=" + id);

    public static Album BuildAlbumFromApiResponse(IEnumerable<AlbumApiResponse> apiResponse)
    {
        var album = new Album();
        return album.Build(apiResponse);
    }
}