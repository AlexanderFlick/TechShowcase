﻿using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PhotoAlbum.Tests.DataTests;

public class AlbumRepoTests : TestBase
{
    private readonly AlbumRepo _sut;
    private readonly Mock<IClientWrapper> _mockClient;
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


        _mockClient = _fixture.Freeze<Mock<IClientWrapper>>();
        _mockClient.Setup(c => c.CreateAlbumApiClient()).Returns(_httpClient);
        _sut = _fixture.Create<AlbumRepo>();
    }

    [Fact]
    public async Task GivenHttpResponse_When200_ThenReturnAlbum()
    {
        var actual = await _sut.ById(_apiResponse.First().AlbumId);

        actual.Should().NotBeNull();
        actual.Photos.Count.Should().Be(_apiResponse.Count());
        actual.Id = _apiResponse.First().Id;
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