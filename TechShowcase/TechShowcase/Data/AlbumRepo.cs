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
        var album = new Album();
        using var client = new HttpClient();
        var uri = "https://jsonplaceholder.typicode.com/photos?albumId=" + id;
        var response = client.GetAsync(uri).Result;

        if (response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var apiResponses = JsonConvert.DeserializeObject<List<AlbumApiResponse>>(content);
            return album.Build(apiResponses);
        }

        return album;
    }
}
