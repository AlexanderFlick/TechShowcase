using AutoFixture;
using FluentAssertions;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Models;
using Xunit;

namespace PhotoAlbum.Tests.DataTests;

public class AlbumRepoTests : TestBase
{
    private readonly AlbumRepo _sut;

    public AlbumRepoTests()
    {
        _sut = _fixture.Create<AlbumRepo>();
    }

    [Fact]
    public void GivenAnId_WhenBuildingUri_ThenBuildCorrectUri()
    {
        var randomAlbumId = _fixture.Create<int>();

        var actual = AlbumRepo.UriBuilder(randomAlbumId);

        actual.Should().Be("https://jsonplaceholder.typicode.com/photos?albumId=" + randomAlbumId);
    }

    [Fact]
    public void GivenId_WhenCallingAlbumApi_ThenReturnCorrectAlbum()
    {
        var validId = _fixture.Create<int>();
        var expected = _fixture.Create<Album>();
        var actual = _sut.ById(validId);

        actual.Should().BeEquivalentTo(expected);
    }
}