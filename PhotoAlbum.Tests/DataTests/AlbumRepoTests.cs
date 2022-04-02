using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PhotoAlbum.Tests.DataTests;

public class AlbumRepoTests : TestBase
{
    private readonly AlbumRepo _sut;
    private readonly Mock<IHttpClientFactory> _mockClient;
    private readonly Mock<HttpMessageHandler> _messageHandler;
    private readonly IEnumerable<AlbumApiResponse> _apiResponse;
    private readonly HttpClient _httpClient;

    public AlbumRepoTests()
    {
        _apiResponse = _fixture.CreateMany<AlbumApiResponse>(2);

        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent(JsonConvert.SerializeObject(_apiResponse))
        };

        _messageHandler = _fixture.Freeze<Mock<HttpMessageHandler>>();
        _messageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
              ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
              ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        _httpClient = new HttpClient(_messageHandler.Object)
        {
            BaseAddress = new Uri("http://cool.domain")
        };

        _mockClient = _fixture.Freeze<Mock<IHttpClientFactory>>();
        _mockClient.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(_httpClient);
        _sut = _fixture.Create<AlbumRepo>();
    }

    [Fact]
    public void GivenCollectionOfApiResponse_ThenBuildAlbum()
    {
        var response = _fixture.CreateMany<AlbumApiResponse>(3);
        var actual = _sut.Build(response);

        actual.Should().NotBeNull();
        actual.Photos.Should().HaveCount(3);
    }
}