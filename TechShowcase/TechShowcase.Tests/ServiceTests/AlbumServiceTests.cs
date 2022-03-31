using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Services;
using System.Linq;
using Xunit;

namespace TechShowcase.Tests.ServiceTests;
public class AlbumServiceTests
{
    private readonly IAlbumService _sut;
    private readonly Mock<IAlbumRepo> _albumRepoMock;
    private readonly Fixture _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());

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
