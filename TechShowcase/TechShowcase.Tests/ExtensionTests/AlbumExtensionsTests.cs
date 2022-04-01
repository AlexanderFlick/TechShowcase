using AutoFixture;
using FluentAssertions;
using PhotoAlbum.Data.Extensions;
using PhotoAlbum.Data.Models;
using Xunit;

namespace PhotoAlbum.Tests.ExtensionTests;

public class AlbumExtensionsTests : TestBase
{
    private readonly Album _sut;

    public AlbumExtensionsTests()
    {
        _sut = new Album { Id = _fixture.Create<int>() };
    }

    [Fact]
    public void GivenCollectionOfApiResponse_ThenBuildAlbum()
    {
        var response = _fixture.CreateMany<AlbumApiResponse>(3);
        var actual = _sut.Build(response);

        actual.Should().NotBeNull();
        actual.Photos.Should().HaveCount(3);
    }

    [Fact]
    public void GivenAnAlbum_WhenWritingToConsole_ThenReturnFormattedHeaderString()
    {
        var actual = _sut.Header();

        actual.Should().Be($"[Album Id: {_sut.Id}]");
    }
}