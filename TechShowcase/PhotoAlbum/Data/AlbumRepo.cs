using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PhotoAlbum.Data.Extensions;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Wrappers;

namespace PhotoAlbum.Data;

public interface IAlbumRepo
{
    Task<Album> ById(int id);
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

    public async Task<Album> ById(int id)
    {
        var photoApiClient = _client.CreateClient("photoAlbumApi");
        var response = await photoApiClient.GetAsync($"photos?albumId={id}");
        response.EnsureSuccessStatusCode();
        
        var content = response.Content.ReadAsStringAsync().Result;
        var objects = JsonConvert.DeserializeObject<List<AlbumApiResponse>>(content);
        return Build(objects!);
    }

    public Album Build(IEnumerable<AlbumApiResponse> apiResponse)
    {
        _album.Id = apiResponse.First().AlbumId;

        apiResponse.ToList().ForEach(x =>
        {
            var photo = new Photo(x.Id, x.Title, x.Url, x.ThumbnailUrl);
            _album.Photos.Add(photo);
        });

        return _album;
    }
}