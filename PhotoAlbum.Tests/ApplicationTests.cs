using AutoFixture;
using Moq;
using PhotoAlbum.Data.Models;
using PhotoAlbum.Services;
using System.Threading.Tasks;
using Xunit;

namespace PhotoAlbum.Tests;

public class ApplicationTests : TestBase
{
    private readonly Application _application;
    private readonly Mock<IConsoleService> _consoleMock;
    private readonly Mock<IAlbumService> _albumServiceMock;

    public ApplicationTests()
    {
        _albumServiceMock = _fixture.Freeze<Mock<IAlbumService>>();
        _consoleMock = _fixture.Freeze<Mock<IConsoleService>>();
        _application = _fixture.Create<Application>();
    }

    [Fact]
    public async Task GivenUserNotFinished_CheckServiceCalls()
    {
        _application.RunApplication();

        _consoleMock.Verify(c => c.PromptNextAlbum(), Times.Once);
        _consoleMock.Verify(c => c.GetAlbumIdFromInput(), Times.Once);
        _consoleMock.Verify(c => c.GiveAlbumOverview(It.IsAny<Album>()), Times.Once);
        _consoleMock.Verify(c => c.WriteAlbumAndPhotoInfo(It.IsAny<Album>()), Times.Once);
        _albumServiceMock.Verify(c => c.ById(It.IsAny<int>()), Times.Once);
    }
}