namespace PhotoAlbum.Wrappers;

public interface IClientWrapper
{
    HttpClient CreateAlbumApiClient();
}
public class ClientWrapper : IClientWrapper
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClientWrapper(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public HttpClient CreateAlbumApiClient()
    {
        return _httpClientFactory.CreateClient("photoAlbumApi");
    }
}
