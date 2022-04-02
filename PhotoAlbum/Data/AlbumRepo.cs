using Newtonsoft.Json;
using PhotoAlbum.Data.Models;

namespace PhotoAlbum.Data;

public interface IAlbumRepo
{
    Task<Album> ById(int id);
}

public class AlbumRepo : IAlbumRepo
{
    private readonly Album _album;
    private readonly IHttpClientFactory _clientFactory;

    public AlbumRepo(IHttpClientFactory client)
    {
        _clientFactory = client;
        _album = new Album();
    }

    public async Task<Album> ById(int id)
    {
        var photoApiClient = _clientFactory.CreateClient("photoAlbumApi");
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