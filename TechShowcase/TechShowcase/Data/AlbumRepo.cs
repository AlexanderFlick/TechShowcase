using Microsoft.AspNetCore.Http;
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
        }
        return new Album();
    }

    public Uri UriBuilder(int id)
    {
        throw new NotImplementedException();
    }

    public Album BuildAlbumFromResponse(List<AlbumApiResponse> albumResponse)
    {
        throw new NotImplementedException();
    }
}
