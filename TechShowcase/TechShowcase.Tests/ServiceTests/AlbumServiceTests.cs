using AutoFixture;
using FluentAssertions;
using Moq;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Services;
using System.Linq;
using Xunit;

namespace PhotoAlbum.Tests.ServiceTests;

public class AlbumServiceTests : TestBase
{
    private readonly IAlbumService _sut;
    private readonly Mock<IAlbumRepo> _albumRepoMock;

    public AlbumServiceTests()
    {
        _albumRepoMock = _fixture.Freeze<Mock<IAlbumRepo>>();
        _sut = _fixture.Create<AlbumService>();
    }

    [Fact]
    public void GivenAlbumId_WhenIdExists_ThenReturnAlbum()
    {
        var randomInt = _fixture.Create<int>();
        var photos = _fixture.CreateMany<Photo>().ToList();
        var validAlbum = new Album
        {
            Id = randomInt,
            Photos = photos
        };
        _albumRepoMock.Setup(c => c.ById(randomInt)).Returns(validAlbum);

        var actual = _sut.ById(randomInt);

        actual.Should().BeEquivalentTo(validAlbum);
    }
}