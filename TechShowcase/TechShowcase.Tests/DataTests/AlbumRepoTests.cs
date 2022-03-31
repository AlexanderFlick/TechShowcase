using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using PhotoAlbum.Data;
using Xunit;

namespace PhotoAlbum.Tests.DataTests;
public class AlbumRepoTests
{
    private readonly AlbumRepo _sut;
    private readonly Fixture _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());

    public AlbumRepoTests()
    {
        _sut = _fixture.Create<AlbumRepo>();
    }

    [Fact]
    public void GivenAnId_WhenBuildingUri_ThenBuildCorrectUri()
    {
        var randomAlbumId = _fixture.Create<int>();

        var actual = _sut.UriBuilder(randomAlbumId);

        actual.Should().Be("https://jsonplaceholder.typicode.com/photos?albumId=" + randomAlbumId);
    }
}
