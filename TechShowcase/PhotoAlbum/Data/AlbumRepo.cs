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
    private readonly Album _album;
    private readonly IHttpClientFactory _client;

    public AlbumRepo(IHttpClientFactory client)
    {
        _client = client;
        _album = new Album();
    }

    public Album ById(int id)
    {
        var photoApiClient = _client.CreateClient("photoAlbumApi");
        var response = photoApiClient.GetAsync($"photos?albumId={id}").Result;
        if (response is not null && response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var objects = JsonConvert.DeserializeObject<List<AlbumApiResponse>>(content);
            return _album.Build(objects!);
        }
        return _album;
    }
}