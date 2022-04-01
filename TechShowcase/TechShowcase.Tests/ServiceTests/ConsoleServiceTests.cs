using AutoFixture;
using FluentAssertions;
using Moq;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Services;
using PhotoAlbum.Wrappers;
using Xunit;

namespace PhotoAlbum.Tests.ServiceTests;

public class ConsoleServiceTests : TestBase
{
    private readonly ConsoleService _sut;
    private readonly Mock<IConsoleWrapper> _consoleMock;

    public ConsoleServiceTests()
    {
        _consoleMock = _fixture.Freeze<Mock<IConsoleWrapper>>();
        _sut = _fixture.Create<ConsoleService>();
    }

    [Fact]
    public void GivenStartOfApplication_ThenReturnANiceGreeting()
    {
        _sut.Greeting();

        _consoleMock.Verify(c => c.Write("Welcome! Give me the number of the photo album that you want to view."), Times.Exactly(1));
    }

    [Fact]
    public void GivenInputFromUser_WhenInvalid_ThenReturnZero()
    {
        var invalidInput = _fixture.Create<string>();
        _consoleMock.Setup(c => c.Read()).Returns(invalidInput);

        var actual = _sut.GetAlbumIdFromInput();

        actual.Should().Be(0);
    }

    [Fact]
    public void GivenInputFromUser_WhenValid_ThenReturnAlbumId()
    {
        var validInput = _fixture.Create<int>();
        _consoleMock.Setup(c => c.Read()).Returns(validInput.ToString());

        var actual = _sut.GetAlbumIdFromInput();

        actual.Should().Be(validInput);
    }

    [Fact]
    public void GivenAnAlbum_WhenPhotosPresent_ThenList()
    {
        var album = _fixture.Create<Album>();
        var totalConsoleWrites = album.Photos.Count + 1;

        _sut.WriteAlbumAndPhotoInfo(album);

        _consoleMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(totalConsoleWrites));
    }
}