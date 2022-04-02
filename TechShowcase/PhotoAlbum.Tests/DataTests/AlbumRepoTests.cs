using AutoFixture;
using Moq;
using PhotoAlbum.Data;
using System.Net.Http;
using Xunit;

namespace PhotoAlbum.Tests.DataTests;

public class AlbumRepoTests : TestBase
{
    private readonly AlbumRepo _sut;
    private Mock<IHttpClientFactory> _mockClient;

    public AlbumRepoTests()
    {
        _mockClient = new Mock<IHttpClientFactory>();
        _sut = _fixture.Create<AlbumRepo>();
    }

    [Fact]
    public void GivenHttpResponse_When()
    {
        
    }
}